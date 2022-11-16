import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SupplierDetailsReadDto } from '../models/dtos/supplierDetailsReadDto';
import { SupplierService } from '../supplier.service';

@Component({
  selector: 'app-supplier-details',
  templateUrl: './supplier-details.component.html',
  styleUrls: ['./supplier-details.component.css']
})
export class SupplierDetailsComponent implements OnInit {

  constructor(private supplierService: SupplierService, private actRoute: ActivatedRoute, private router: Router) { }

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
    
  }

  DeleteSupplier() {
    this.supplierService.deleteSupplier(this.id).subscribe({
      next: response => console.log(response),
      error: err => console.log(err),
      complete: () => {
        window.alert("Supplier deleted Successfully"),
          this.router.navigate(['supplier'])
      }
    });
  }

  enableOrDisableSupplier = (form: NgForm) => {
    if (form.valid) {
      
      this.supplierService.patchSupplier(this.id, this.supplier).subscribe({
        next: response => {
          console.log(response);
        },
        error: err => console.log(err)
      });
    }
  }
}
