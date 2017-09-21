using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlpineSkiHouse.Web.Data;
using Microsoft.AspNetCore.Identity;
using AlpineSkiHouse.Web.Models;
using AlpineSkiHouse.Web.Models.SkiCardViewModels;
using Microsoft.EntityFrameworkCore;

namespace AlpineSkiHouse.Web.Controllers
{
    [Authorize]
    public class SkiCardController : Controller
    {
        private readonly SkiCardContext _skiCardContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public SkiCardController(SkiCardContext skiCardContext, UserManager<ApplicationUser> userManager)
        {
            _skiCardContext = skiCardContext;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var skiCardsViewModels = await _skiCardContext.SkiCards
                .Where(s => s.ApplicationUserId == userId)
                .Select(s => new SkiCardListViewModel
                {
                    Id = s.Id,
                    CardHolderName = s.CardHolderFirstName + " " + s.CardHolderLastName
                }).ToListAsync();

            return View(skiCardsViewModels);
        }

        public async Task<ActionResult> Create()
        {
            var userId = _userManager.GetUserId(User);
            var currentUser = await _userManager.FindByIdAsync(userId);
            var viewModel = new CreateSkiCardViewModel
            {
                CardHolderPhoneNumber = currentUser.PhoneNumber
            };

            var hasExistingSkiCards = _skiCardContext.SkiCards.Any(s => s.ApplicationUserId == userId);

            if(!hasExistingSkiCards)
            {
                viewModel.CardHolderFirstName = currentUser.FirstName;
                viewModel.CardHolderLastName = currentUser.LastName;
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSkiCardViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                SkiCard skiCard = new SkiCard
                {
                    ApplicationUserId = userId,
                    CreatedOn = DateTime.UtcNow,
                    CardHolderFirstName = viewModel.CardHolderFirstName,
                    CardHolderLastName = viewModel.CardHolderLastName,
                    CardHolderBirthDate = viewModel.CardHolderBirthDate.Value.Date,
                    CardHolderPhoneNumber = viewModel.CardHolderPhoneNumber
                };

                _skiCardContext.SkiCards.Add(skiCard);

                await _skiCardContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);

            var skiCardViewModel = await _skiCardContext.SkiCards
                .Where(s => s.ApplicationUserId == userId && s.Id == id)
                .Select(s => new EditSkiCardViewModel
                {
                    Id = s.Id,
                    CardHolderFirstName = s.CardHolderFirstName,
                    CardHolderLastName = s.CardHolderLastName,
                    CardHolderBirthDate = s.CardHolderBirthDate,
                    CardHolderPhoneNumber = s.CardHolderPhoneNumber
                }).SingleOrDefaultAsync();

            if(skiCardViewModel == null)
            {
                return NotFound();
            }

            return View(skiCardViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSkiCardViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                var skiCard = await _skiCardContext.SkiCards
                    .SingleOrDefaultAsync(s => s.ApplicationUserId == userId && s.Id == viewModel.Id);

                if(skiCard == null)
                {
                    return NotFound();
                }

                skiCard.CardHolderFirstName = viewModel.CardHolderFirstName;
                skiCard.CardHolderLastName = viewModel.CardHolderLastName;
                skiCard.CardHolderPhoneNumber = viewModel.CardHolderPhoneNumber;
                skiCard.CardHolderBirthDate = viewModel.CardHolderBirthDate.Value.Date;

                await _skiCardContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
    }
}