export class Result<T> {
  data: T | undefined
  code: string | undefined
  success = false

  // construct an instance for fail result
  constructor(code: string) {
    this.code = code
  }
}

export type Paging<T> = {
  items: T[]
  total: number
  current: number
}

export type PagingRequest = {
  search: string
  sortBy: string
  sortDir: string
  current: number
  take: number
}


