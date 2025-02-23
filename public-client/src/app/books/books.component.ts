import { Component, inject, OnInit } from '@angular/core';
import { BooksService } from '../books.service';
import { AsyncPipe, JsonPipe, NgFor, NgForOf } from '@angular/common';

@Component({
  selector: 'app-books',
  imports: [AsyncPipe, NgFor, NgForOf],
  templateUrl: './books.component.html',
  styleUrl: './books.component.css'
})
export class BooksComponent {
  booksService : BooksService = inject(BooksService);
  books$ = this.booksService.getBooks();
}