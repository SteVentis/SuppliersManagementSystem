import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Category } from './models/category.model';
import { Country } from './models/country.model';
import { SupplierReadDto } from './models/dtos/supplierReadDto';
import { Supplier } from './models/supplier.model';
import { SupplierService } from './supplier.service';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.css']
})
export class SupplierComponent implements OnInit {
  
  suppliers!: Supplier[];
  countries!: Country[];
  categories!: Category[];
  createHidden: boolean = false;
  buttonDetailsHidden: boolean = false;
  tableDetailsHidden: boolean = false;

  detailsOfSupplier: SupplierReadDto = {
      name: '',
      categoryName: '',
      taxIdentNumber: 0,
      address: '',
      countryName: '',
      phone: '',
      email: ''
  }

  toggleCreate() {
    this.createHidden = !this.createHidden;
  }
  detailsButtonHidden() {
    this.buttonDetailsHidden = !this.createHidden;
  }

  tableDetailsOfSupplier() {
    this.tableDetailsHidden = !this.tableDetailsHidden;
  }

  constructor(private supplierService: SupplierService, private jwtHelper: JwtHelperService, private router: Router) { }

  ngOnInit(): void {
    this.supplierService.getSuppliers().subscribe({
      next: response => {
        this.suppliers = response;
      },
      error: e => console.log(e),
      complete: () => console.table(this.suppliers)
    });
    
  }
  isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    return false
  }

  logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(["/login"]);
  }

  
}
