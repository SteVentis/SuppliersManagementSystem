import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../models/category.model';
import { Country } from '../models/country.model';
import { SupplierDetailsReadDto } from '../models/dtos/supplierDetailsReadDto';
import { SupplierService } from '../supplier.service';

@Component({
  selector: 'app-supplier-update',
  templateUrl: './supplier-update.component.html',
  styleUrls: ['./supplier-update.component.css']
})
export class SupplierUpdateComponent implements OnInit {

  constructor(private supplierService: SupplierService, private actRoute: ActivatedRoute, private router: Router) { }

  countries!: Country[];
  categories!: Category[]
  id = this.actRoute.snapshot.params['id'];
  supplier: SupplierDetailsReadDto = {
    id: 0,
    name: '',
    categoryId: 0,
    categoryName: '',
    taxIdentNumber: 0,
    address: '',
    countryId: 0,
    countryName: '',
    phone: '',
    email: '',
    isActive: false
  };



  ngOnInit(): void {
    this.supplierService.getDetailsOfSupplier(this.id).subscribe({
      next: response => {
        this.supplier = response;
      },
      error: err => console.log(err),
      complete: () => console.log(this.supplier)
    });
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

  UpdateSupplier = (form: NgForm) => {
    if (form.valid) {
      this.supplierService.updateSupplier(this.id, this.supplier).subscribe({
        next: response => {
          console.log(response);
        },
        error: err => console.log(err),
        complete: () => {
          window.alert("Supplier Updated Successfully"),
          this.router.navigate(['supplier'])
        }
      });
    }
  }

}
