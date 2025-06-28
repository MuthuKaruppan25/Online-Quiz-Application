import { Component, OnInit } from '@angular/core';
import { Admin, Attender } from '../Models/UserModel';
import { Store } from '@ngrx/store';
import { selectAdminProfile } from '../store/Admin/admin.selectors';
import {
  selectAttenderError,
  selectAttenderProfile,
} from '../store/User/user.selectors';

@Component({
  selector: 'app-profile-user',
  imports: [],
  templateUrl: './profile-user.html',
  styleUrl: './profile-user.css',
})
export class ProfileUser implements OnInit {
  username: string = '';
  role: string = '';
  userData!: Admin | Attender;

  constructor(private store: Store) {
    this.username = localStorage.getItem('username') ?? '';
    this.role = localStorage.getItem('role') ?? '';
  }
  ngOnInit(): void {
    if (this.role === 'admin') {
      this.store.select(selectAdminProfile).subscribe({
        next: (data: any) => {
          this.userData = data as Admin;
        },
      });
    } else {
      this.store.select(selectAttenderProfile).subscribe({
        next: (data: any) => {
          this.userData = data as Attender;
        },
      });
    }
  }
}
