import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { Card } from '../models/card';

@Injectable({
  providedIn: 'root'
})
export class CardsService {
  baseUrl = 'https://localhost:7120/api/cards'
  constructor(private http: HttpClient) { }

  getAllCards():Observable<Card[]> {
    return this.http.get<Card[]>(this.baseUrl);
  }

  AddCard(card:Card): Observable<Card> {
    card.id = '00000000-0000-0000-0000-000000000000'
    return this.http.post<Card>(this.baseUrl,card);
  }

  DeleteCard(id:string) : Observable<Card> {
    return this.http.delete<Card>(this.baseUrl + '/' + id);
  }

  UpdateCard(card:Card) : Observable<Card> {
    return this.http.put<Card>(this.baseUrl + '/' + card.id,card);
  }
}
