export interface SupplierDetailsReadDto {
  id: number,
  name: string,
  categoryId: number,
  categoryName: string,
  taxIdentNumber: number,
  address: string,
  countryId: number,
  countryName: string,
  phone: string,
  email: string,
  isActive: boolean
}
