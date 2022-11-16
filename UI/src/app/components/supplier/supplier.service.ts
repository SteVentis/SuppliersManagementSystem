import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from './models/category.model';
import { Country } from './models/country.model';
import { SupplierCreateDto } from './models/dtos/supplierCreateDto';
import { SupplierDetailsReadDto } from './models/dtos/supplierDetailsReadDto';
import { Supplier } from './models/supplier.model';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {


  constructor(private httpService: HttpClient) { }

  private url = "https://localhost:44309/api/Supplier/";
  private urlForCountries = "https://localhost:44309/api/Country/";
  private urlForCategories = "https://localhost:44309/api/Category/";

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  getSuppliers(): Observable<Supplier[]> {
    return this.httpService.get<Supplier[]>(this.url);
  }

  getDetailsOfSupplier(id: number): Observable<SupplierDetailsReadDto> {
    return this.httpService.get<SupplierDetailsReadDto>(this.url + id)
  }

  insertSupplier(supplier: SupplierCreateDto): Observable<SupplierCreateDto> {
    return this.httpService.post<Supplier>(this.url, supplier, this.httpOptions);
  }

  updateSupplier(id: number, supplier: SupplierDetailsReadDto): Observable<SupplierDetailsReadDto> {
    return this.httpService.put<SupplierDetailsReadDto>(this.url + id, supplier, this.httpOptions);
  }

  deleteSupplier(id: number) {
    return this.httpService.delete<Supplier>(this.url + id, this.httpOptions);
  }

  patchSupplier(id: number, supplier: SupplierDetailsReadDto): Observable<SupplierDetailsReadDto> {
    return this.httpService.patch<SupplierDetailsReadDto>(this.url + id, supplier, this.httpOptions);
  }

  getAllCountries(): Observable<Country[]> {
    return this.httpService.get<Country[]>(this.urlForCountries);
  }
  getAllCategories(): Observable<Category[]>{
    return this.httpService.get<Category[]>(this.urlForCategories);
  }
  
}
