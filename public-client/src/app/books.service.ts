import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs'; // Import Observable for handling async data
import { Book } from './models/book';

@Injectable({
  providedIn: 'root',
})
export class BooksService {
  private apiUrl = 'https://localhost:5000/books/'; // Example API URL
  constructor(private http: HttpClient) {}
  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl); 
  }
}
