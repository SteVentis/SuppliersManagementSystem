import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { SupplierComponent } from './components/supplier/supplier.component';
import { AuthGuard } from './guards/auth.guard';
import { SupplierDetailsComponent } from './components/supplier/supplier-details/supplier-details.component';
import { SupplierUpdateComponent } from './components/supplier/supplier-update/supplier-update.component';


const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'supplier', component: SupplierComponent, canActivate: [AuthGuard] },
  { path: 'details/:id', component: SupplierDetailsComponent, canActivate: [AuthGuard] },
  { path: 'update/:id', component: SupplierUpdateComponent, canActivate: [AuthGuard] }
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
