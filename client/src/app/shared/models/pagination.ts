import { Product } from "./product";

export interface Paginaton<T> {
  pageIndex: number
  pageSize: number
  count: number
  data: T
}
