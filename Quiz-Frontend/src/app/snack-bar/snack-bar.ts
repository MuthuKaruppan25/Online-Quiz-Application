import { Component, Input, OnChanges } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-snack-bar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './snack-bar.html',
  styleUrl: './snack-bar.css'
})
export class SnackBar implements OnChanges {
  @Input() message: string = '';
  @Input() isSuccess: boolean = true;

  visible: boolean = false;

  ngOnChanges() {
    if (this.message) {
      this.visible = true;
      setTimeout(() => {
        this.visible = false;
      }, 3000);
    }
  }
}
