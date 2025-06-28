import { Component, OnInit, HostListener } from '@angular/core';
import { Attempts } from '../Models/AttemptorAttempts';
import { UserService } from '../services/UserService';
import { pagination } from '../Models/pagination';
import { Attender } from '../Models/UserModel';
import { Store } from '@ngrx/store';
import { selectAttenderProfile } from '../store/User/user.selectors';
import { CommonModule } from '@angular/common';
import { HistoryCard } from "../history-card/history-card";
import { LucideAngularModule,History } from 'lucide-angular';

interface PagedResult<T> {
  totalCount: number;
  data: T[];
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

@Component({
  selector: 'app-user-history',
  standalone: true,
  imports: [CommonModule, HistoryCard,LucideAngularModule],
  templateUrl: './user-history.html',
  styleUrl: './user-history.css'
})
export class UserHistory implements OnInit {
  attemptsList: Attempts[] = [];
  pageNumber: number = 1;
  pageSize: number = 10;
  totalPages: number = 1;
  isLoading: boolean = false;
  readonly history = History;

  attender!: Attender;

  constructor(private userService: UserService, private store: Store) {}

  ngOnInit(): void {
    this.store.select(selectAttenderProfile).subscribe({
      next: (data: any) => {
        this.attender = data as Attender;
        this.loadAttempts(); 
      }
    });
  }

  loadAttempts() {
    if (this.isLoading || this.pageNumber > this.totalPages) return;

    this.isLoading = true;

    const pageData = new pagination(this.pageNumber, this.pageSize);

    this.userService.getAttempts(pageData, this.attender.guid).subscribe({
      next: (data: PagedResult<Attempts>) => {
        this.attemptsList.push(...data.data);
        this.totalPages = data.totalPages;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading attempts', err);
        this.isLoading = false;
      }
    });
  }

  @HostListener('window:scroll', [])
  onScroll() {
    if (
      window.innerHeight + window.scrollY >= document.body.offsetHeight - 200
    ) {
      if (!this.isLoading && this.pageNumber < this.totalPages) {
        this.pageNumber++;
        this.loadAttempts();
      }
    }
  }
}
