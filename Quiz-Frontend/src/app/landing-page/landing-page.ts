import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing-page',
  imports: [FormsModule],
  templateUrl: './landing-page.html',
  styleUrl: './landing-page.css'
})
export class LandingPage {
  selectedrole ="user";
  constructor(private router:Router)
  {

  }
  goToLogin() {
    this.router.navigate(['/login'], {
      queryParams: { role: this.selectedrole }
    });
  }
}
