import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserRegister } from '../Models/UserModel';
import { UserService } from '../services/UserService';
import { AdminService } from '../services/AdminService';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
})
export class Login {
  loginForm: FormGroup;
  role: string = '';
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private adminService: AdminService
  ) {
    this.loginForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required),
    });
  }

  ngOnInit() {
    this.role = this.route.snapshot.queryParamMap.get('role') || 'user';
    console.log(this.role);
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const userData = new UserRegister({
      username: this.loginForm.value.email,
      password: this.loginForm.value.password,
      firstName: this.loginForm.value.firstName,
      lastName: this.loginForm.value.lastName,
      role: this.role,
    });

    this.successMessage = null;
    this.errorMessage = null;

    if (userData.role === 'admin') {
      this.adminService.RegisterAdmin(userData).subscribe({
        next: (data: any) => {
          this.successMessage = 'Login successful!';
          this.errorMessage = null;
          localStorage.setItem('username', userData.username);
          localStorage.setItem('role', userData.role);

          setTimeout(() => {
            this.router.navigate(['/dashboard']);
          }, 1500);
        },
        error: () => {
          this.errorMessage = 'Login failed. Please check your credentials.';
          this.successMessage = null;
        },
      });
    } else {
      this.userService.RegisterUser(userData).subscribe({
        next: (data) => {
          this.successMessage = 'Login successful!';
          this.errorMessage = null;
          localStorage.setItem('username', userData.username);
          localStorage.setItem('role', userData.role);

          setTimeout(() => {
            this.router.navigate(['/dashboard']);
          }, 1500);
        },
        error: () => {
          this.errorMessage = 'Login failed. Please check your credentials.';
          this.successMessage = null;
        },
      });
    }
  }
}
