export interface SupplierCreateDto {
  name: string,
  categoryId: number,
  taxIdentNumber: number,
  address: string,
  countryId: number,
  phone: string,
  email: string,
  isActive: boolean
}
