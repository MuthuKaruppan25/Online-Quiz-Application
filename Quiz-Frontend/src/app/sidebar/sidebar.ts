import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { LucideAngularModule,LayoutDashboardIcon,FilePlus,List,User,Play,History, BookOpenText } from 'lucide-angular';

@Component({
  selector: 'app-sidebar',
  imports: [LucideAngularModule, CommonModule,RouterLink,RouterLinkActive ],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css'
})
export class Sidebar implements OnInit {
  readonly dashboard = LayoutDashboardIcon;
  readonly file = FilePlus;
  readonly list = List;
  readonly user = User;
  readonly play = Play;
  readonly history = History;
  readonly open = BookOpenText;

  @Input() role:string = "";

  ngOnInit(): void {
    this.role = localStorage.getItem('role') ?? '';
    console.log(this.role);
  }
}
