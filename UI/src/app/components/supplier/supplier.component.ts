import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Supplier } from './supplier.model';
import { SupplierService } from './supplier.service';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.css']
})
export class SupplierComponent implements OnInit {
  dt: any;
  dataDisplay: any;
  suppliers!: Array<Supplier>;

  constructor(private supplierService: SupplierService) { }

  ngOnInit(): void {
    this.supplierService.getSuppliers().subscribe({
      next: response => {
        this.suppliers = response;
        this.dt = response;
        this.dataDisplay = this.dt.data;
      },
      error: e => console.log(e),
      complete: () => console.log(this.suppliers)
    });

  }
  //isUserAuthenticated = (): boolean => {
  //  const token = localStorage.getItem("jwt");
  //  if (token && !this.jwtHelper.isTokenExpired(token)) {
  //    return true;
  //  }
  //  return false;
  //}
  //logOut = () => {
  //  localStorage.removeItem("jwt");
  //}
}
