import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Category } from '../models/category.model';
import { Country } from '../models/country.model';
import { SupplierCreateDto } from '../models/dtos/supplierCreateDto';
import { SupplierService } from '../supplier.service';

@Component({
  selector: 'app-supplier-create',
  templateUrl: './supplier-create.component.html',
  styleUrls: ['./supplier-create.component.css']
})
export class SupplierCreateComponent implements OnInit {

  supplierToBeInserted: SupplierCreateDto = {
    name: '',
    categoryId: 0,
    taxIdentNumber: 0,
    address: '',
    countryId: 0,
    phone: '',
    email: '',
    isActive: true
  }
  selectedValue: any;

  countries!: Country[];
  categories!: Category[];
  constructor(private supplierService: SupplierService) { }

  ngOnInit(): void {

    this.supplierService.getAllCountries().subscribe({
      next: response => {
        this.countries = response;
      },
      error: e => console.log(e),
      complete: () => console.table(this.countries)
    });
    this.supplierService.getAllCategories().subscribe({
      next: response => {
        this.categories = response;
      },
      error: e => console.log(e),
      complete: () => console.table(this.categories)
    });
  }
  InsertSupplier = (form: NgForm) => {
    if (form.valid) {
      this.supplierService.insertSupplier(this.supplierToBeInserted).subscribe({
        next: response => console.log(response),
        error: err => console.log(err),
        complete: () => {
          window.alert("Supplier inserted Successfully"),
            window.location.reload()
        }
      });
    }
  }
  
}
