﻿using Bogus;
using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Db;
using EmployeeManagement.Core.Exceptions;
using EmployeeManagement.Core.Extensions;
using EmployeeManagement.Core.Services;
using EmployeeManagement.Shared.Entities;
using EmployeeManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Core.Implementations;

public class EmployeeService(IDbContextFactory<ApplicationDbContext> dbContextFactory) : IEmployeeService
{
    public async Task<Result<EmployeeModel>> AddEmployeeAsync(EmployeeModel employee)
    {
        if (employee is null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        using var db = dbContextFactory.CreateDbContext();

        var emailExist = await db.Employees.AnyAsync(e => e.Email == employee.Email);
        if (emailExist)
        {
            return Result<EmployeeModel>.Fail(ResultCode.EmailExist);
        }

        var entity = db.Employees.Add(new Employee
        {
            Email = employee.Email,
            Name = employee.Name,
            Gender = employee.Gender,
            Phone = employee.Phone,
            Dob = employee.Dob,
            Address = employee.Address
        }).Entity;
        await db.SaveChangesAsync();

        return new Result<EmployeeModel>(new EmployeeModel
        {
            Id = entity.Id,
            Email = entity.Email,
            Name = entity.Name,
            Gender = entity.Gender,
            Phone = entity.Phone,
            Dob = entity.Dob,
            Address = entity.Address
        });
    }

    public async Task<Result<List<EmployeeModel>>> GenerateRandomEmployeesAsync(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentException("Count must be greater than zero", nameof(count));
        }

        using var db = dbContextFactory.CreateDbContext();

        var faker = new Faker<Employee>()
            .RuleFor(e => e.Name, f => f.Name.FullName())
            .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.Name))
            .RuleFor(e => e.Gender, f => f.PickRandom<string>("Male", "Female"))
            .RuleFor(e => e.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(e => e.Dob, f => f.Date.Past(30, DateTime.Now.AddYears(-20)))
            .RuleFor(e => e.Address, f => f.Address.FullAddress());

        var randomEmployees = faker.Generate(count);

        db.Employees.AddRange(randomEmployees);
        await db.SaveChangesAsync();

        var result = randomEmployees.Select(e => new EmployeeModel
        {
            Id = e.Id,
            Name = e.Name,
            Email = e.Email,
            Gender = e.Gender,
            Phone = e.Phone,
            Dob = e.Dob,
            Address = e.Address
        }).ToList();

        return new Result<List<EmployeeModel>>(result);
    }

    public async Task<EmployeeModel> GetEmployeeByIdAsync(int id)
    {
        using var db = dbContextFactory.CreateDbContext();
        var employee = await db.Employees
        .Where(e => e.Id == id)
        .Select(e => new EmployeeModel
        {
            Id = e.Id,
            Email = e.Email,
            Name = e.Name,
            Gender = e.Gender,
            Phone = e.Phone,
            Dob = e.Dob,
            Address = e.Address
        })
        .FirstOrDefaultAsync() ?? throw new EntityNotFoundException(nameof(Employee), id);

        return employee;
    }

    public async Task<Paging<EmployeeModel>> GetEmployeeListAsync(PagingRequest? filter = null)
    {
        filter ??= new();

        using var db = dbContextFactory.CreateDbContext();
        var current = filter.Current;
        var take = filter.PerPage;
        var query = db.Employees.OrderByDescending(e => e.Id).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var q = filter.Search.Trim().ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(q) || x.Email.Contains(q));
        }

        if (!string.IsNullOrWhiteSpace(filter.SortBy))
        {
            var sort = filter.SortBy.Trim();
            query = sort switch
            {
                nameof(Employee.Name) => query.Sort(e => e.Name, filter.IsAsc),
                nameof(Employee.Email) => query.Sort(x => x.Email, filter.IsAsc),
                _ => query
            };
        }

        var paging = await query.ToPagingAsync(e => new EmployeeModel
        {
            Id = e.Id,
            Email = e.Email,
            Name = e.Name,
            Gender = e.Gender,
            Phone = e.Phone,
            Dob = e.Dob,
            Address = e.Address
        }, current, take);

        return paging;
    }
}
