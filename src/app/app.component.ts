import { Component, OnInit } from '@angular/core';
import { Card } from './models/card';
import { CardsService } from './service/cards.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Cards';
  cards: Card[] = [];
  card: Card = {
    id: '',
    cardholderName: '',
    cardNumber: '',
    expiryMonth: '',
    expiryYear: '',
    cvv:''
  }
  constructor(private cardsService:CardsService) {

  }
  ngOnInit(): void {
    this.getAllCards();
  }

  getAllCards() {
    this.cardsService.getAllCards().subscribe(
      response => {
        this.cards = response;
      }
    );
  }


  OnSubmit() {
    if(this.card.id === '')
    {
      this.cardsService.AddCard(this.card)
    .subscribe(
      response=> {
        this.getAllCards();
        this.card = {
          id: '',
          cardholderName: '',
          cardNumber: '',
          expiryMonth: '',
          expiryYear: '',
          cvv:''
        }
      }
    )
    }
  else{
    this.UpdateCard(this.card);
  }
    
  }

  deleteCard(id:string) {
    this.cardsService.DeleteCard(id)
    .subscribe(
      Response => {
        this.getAllCards();
      }
    )}

  populateForm(card : Card) {
    this.card = card;
    }

    UpdateCard(card:Card)
    {
      this.cardsService.UpdateCard(card)
      .subscribe(
        response => {
          this.getAllCards();
        }
      )
    }
}
