using Cards.Api.Data;
using Cards.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext _cardsDbContext;
        public CardsController(CardsDbContext cardsDbContext)
        {
            this._cardsDbContext = cardsDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {

           var cards =  await _cardsDbContext.Cards.ToListAsync();
            return Ok(cards);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {

            var card = await _cardsDbContext.Cards.FirstOrDefaultAsync(y=> y.Id == id);
            if (card != null)
                return Ok(card);
            else
                return NotFound("Card not found");
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();
            await _cardsDbContext.AddAsync(card);
            await _cardsDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), new { id = card.Id },card);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCard([FromBody] Card card, [FromRoute] Guid id)
        {

            var existingCard = await _cardsDbContext.Cards.FirstOrDefaultAsync(y => y.Id == id);
            if (existingCard != null)
            {
                existingCard.CardholderName = card.CardholderName;
                existingCard.CardNumber = card.CardNumber;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                existingCard.CVV = card.CVV;
                await _cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            else
                return NotFound("Card not found");
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {

            var existingCard = await _cardsDbContext.Cards.FirstOrDefaultAsync(y => y.Id == id);
            if (existingCard != null)
            {
                _cardsDbContext.Remove(existingCard);
                await _cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            else
                return NotFound("Card not found");
        }
    }
}
