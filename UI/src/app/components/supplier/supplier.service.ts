import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Supplier } from './supplier.model';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {


  constructor(private httpService: HttpClient) { }

  private url = "https://localhost:44309/api/Supplier/";

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  getSuppliers(): Observable<Supplier[]> {
    return this.httpService.get<Supplier[]>(this.url);
  }

  getDetailsOfSupplier(id: number): Observable<Supplier> {
    return this.httpService.get<Supplier>(this.url + id)
  }

  insertSupplier(supplier: Supplier): Observable<Supplier> {
    return this.httpService.post<Supplier>(this.url, supplier, this.httpOptions);
  }

  updateSupplier(id: number, supplier: Supplier): Observable<Supplier> {
    return this.httpService.put<Supplier>(this.url + id, supplier, this.httpOptions);
  }

  deleteSupplier(id: number): Observable<Supplier> {
    return this.httpService.delete<Supplier>(this.url + id, this.httpOptions);
  }
}
