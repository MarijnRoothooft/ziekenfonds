// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Project_ZF.Models;

namespace Project_ZF.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;

        public IndexModel(
            UserManager<CustomUser> userManager,
            SignInManager<CustomUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Telefoonnummer")]
            [Required(ErrorMessage = "Vul een telefoonnummer in aub")]

            public string PhoneNumber { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een naam in aub")]
            [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]
            public string? Naam { get; set; }

            [PersonalData]
            [DataType(DataType.Date, ErrorMessage = "Ongeldige datum.")]
            [Required(ErrorMessage = "Vul een geboortedatum in aub")]

            public DateTime Geboortedatum { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een huisdokter in aub")]

            public string? Huisdokter { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een voornaam in aub")]
            [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]


            public string? Voornaam { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een straat in aub")]

            public string? Straat { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een huisnummer in aub")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Enkel getallen zijn toegelaten.")]


            public string? Huisnummer { get; set; }
            [PersonalData]
            [CustomRequired("EmailAdress")]

            public string? Email { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een postcode in aub")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Enkel getallen zijn toegelaten.")]


            public string? Postcode { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een gemeente in aub")]

            public string? Gemeente { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een telefoonnummer in aub")]

            public string? TelefoonNummer { get; set; }

        }
        
        private async Task LoadAsync(CustomUser user)

        {

            var userName = await _userManager.GetUserNameAsync(user);

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var Naam = await Task.FromResult(user.Naam);
            var Voornaam = await Task.FromResult(user.Voornaam);
            var Straat = await Task.FromResult(user.Straat);
            var Huisnummer = await Task.FromResult(user.Huisnummer);
            var Gemeente = await Task.FromResult(user.Gemeente);
            var Postcode =await Task.FromResult(user.Postcode);
            var Geboortedatum = await Task.FromResult(user.GeboorteDatum);
            var Huisdokter = await Task.FromResult(user.Huisdokter);
            var Email= await Task.FromResult(user.Email);
            var TelefoonNummer = await Task.FromResult(user.PhoneNumber);
            

            DateTime Datum = Geboortedatum?? new DateTime(1970, 1, 1);

            Username = userName;

            Input = new InputModel

            {

                PhoneNumber = TelefoonNummer,
                Naam = Naam,
                Geboortedatum = Datum,
                Voornaam =Voornaam,
                Straat = Straat,
                Huisnummer = Huisnummer,
                Gemeente = Gemeente,
                Postcode = Postcode,
                Huisdokter = Huisdokter,
                Email = Email,
                TelefoonNummer= TelefoonNummer,

            };

        }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Niet mogelijk om de gebruiker met id: ' {_userManager.GetUserId(User)} ' op te halen.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Niet mogelijk om de gebruiker met id: ' {_userManager.GetUserId(User)} ' op te halen.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Onverwachte error wanneer de telefoonnummer wordt veranderd.";
                    return RedirectToPage();
                }
            }
            user.Naam = Input.Naam;
            user.Voornaam = Input.Voornaam;
            user.GeboorteDatum = Input.Geboortedatum;
            user.Straat= Input.Straat;
            user.Postcode = Input.Postcode;
            user.TelefoonNummer = Input.TelefoonNummer;
            user.PhoneNumber = Input.TelefoonNummer;
            user.Huisnummer = Input.Huisnummer;
            user.Huisdokter = Input.Huisdokter;
            

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Je profiel is aangepast'";
            return RedirectToPage();
        }
    }
}
