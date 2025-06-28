import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Sidebar } from '../sidebar/sidebar';
import { RouterOutlet } from '@angular/router';
import { UserService } from '../services/UserService';
import { AdminService } from '../services/AdminService';
import { Store } from '@ngrx/store';
import { loadAttenderProfileSuccess } from '../store/User/user.actions';
import { Admin, Attender } from '../Models/UserModel';
import { selectAdminProfile } from '../store/Admin/admin.selectors';
import { loadAdminProfileSuccess } from '../store/Admin/admin.actions';
import { selectAttenderError, selectAttenderProfile } from '../store/User/user.selectors';
import { Navbar } from "../navbar/navbar";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [Sidebar, RouterOutlet, Navbar],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  username: string = '';
  role: string = '';

  adminData!: Admin;

  constructor(
    private router: Router,
    private userService: UserService,
    private adminService: AdminService,
    private store: Store
  ) {
    this.username = localStorage.getItem('username') ?? '';
    this.role = localStorage.getItem('role') ?? '';
    console.log(this.username);
  }

  ngOnInit(): void {
    if (this.role === 'user' && this.username) {
      this.userService.getUser(this.username).subscribe({
        next: (data: any) => {
          console.log(data);
          this.store.dispatch(
            loadAttenderProfileSuccess({ profile: data as Attender })
          );
          this.store.select(selectAttenderProfile).subscribe({
            next: (data) => {
              console.log(data);
            },
          });
        },
        error: (err) => {
          console.error('Error fetching user data:', err);
        },
      });
    } else {
      this.adminService.getAdmin(this.username).subscribe({
        next: (data: any) => {
          this.store.dispatch(
            loadAdminProfileSuccess({ profile: data as Admin })
          );
          this.store.select(selectAdminProfile).subscribe({
            next: (data) => {
              console.log(data);
            },
          });
        },
        error: (err) => {
          console.error('Error fetching user data:', err);
        },
      });
    }
  }
}
