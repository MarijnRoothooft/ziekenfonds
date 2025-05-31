// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace Project_ZF.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IUserStore<CustomUser> _userStore;
        private readonly IUserEmailStore<CustomUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<CustomUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<CustomUser> userStore,
            SignInManager<CustomUser> signInManager,
            ILogger<RegisterModel> logger,
          IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

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
        /// 
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

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
            [CustomRequired("")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Vul een wachtwoord in aub")]

            [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} en mag maximum {1} karaters lang zijn.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "wachtwoord")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Vul een wachtwoord in aub")]
            [DataType(DataType.Password)]
            [Display(Name = "Herhaal wachtwoord")]
            [Compare("Password", ErrorMessage = "Het wachtwoord en herhaalde wachtwoord komen niet overeen.")]
            public string ConfirmPassword { get; set; }
            [Required(ErrorMessage = "Vul een telefoonnummer in aub")]
            [Display(Name = "Telefoonnummer")]
            public string PhoneNumber { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een Familienaam in aub")]

            public string? Naam { get; set; }
            [PersonalData]
            [DataType(DataType.Date, ErrorMessage = "Ongeldige datum.")]
            [Required(ErrorMessage = "Vul een geboortedatum in aub")]

            public DateTime Geboortedatum { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een Huisdokter in aub")]
            [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]


            public string? Huisdokter { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een voornaam in aub")]

            public string? Voornaam { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een straat in aub")]
            public string? Straat { get; set; }

            [PersonalData]
            [Range(1, int.MaxValue, ErrorMessage = "Alleen positieve nummers mogelijk")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Enkel getallen zijn toegelaten.")]
            [Required(ErrorMessage = "Vul een huisnummer in aub")]
            public string? Huisnummer { get; set; }

            [Range(1000, 10000, ErrorMessage = "Geef een geldige Belgische postcode")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Enkel getallen zijn toegelaten.")]
            [Required(ErrorMessage = "Vul een postcode in aub")]
            [PersonalData]

            public string? Postcode { get; set; }
            [PersonalData]
            [Required(ErrorMessage = "Vul een gemeente in aub")]

            public string? Gemeente { get; set; }
            [PersonalData]
            [RegularExpression(@"^\+32 \\d{3} \d{2}-\d{2}$", ErrorMessage = "geen geldig telefoonnummer.")]


            public string? TelefoonNummer { get; set; }

        }
        [AllowAnonymous]

        private async Task LoadAsync(CustomUser user)

        {

            var userName = await _userManager.GetUserNameAsync(user);

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var Naam = await Task.FromResult(user.Naam);
            var Voornaam = await Task.FromResult(user.Voornaam);
            var Straat = await Task.FromResult(user.Straat);
            var Huisnummer = await Task.FromResult(user.Huisnummer);
            var Gemeente = await Task.FromResult(user.Gemeente);
            var Postcode = await Task.FromResult(user.Postcode);
            var Geboortedatum = await Task.FromResult(user.GeboorteDatum);
            var Huisdokter = await Task.FromResult(user.Huisdokter);
            var TelefoonNummer = await Task.FromResult(user.PhoneNumber);


            DateTime Datum = Geboortedatum ?? new DateTime(1970, 1, 1);

            Input = new InputModel

            {

                PhoneNumber = phoneNumber,
                Naam = Naam,
                Geboortedatum = Datum,
                Voornaam = Voornaam,
                Straat = Straat,
                Huisnummer = Huisnummer,
                Gemeente = Gemeente,
                Postcode = Postcode,
                Huisdokter = Huisdokter,
                TelefoonNummer = phoneNumber,
                

            };
        }

        [AllowAnonymous]

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        [AllowAnonymous]
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();


            if (ModelState.IsValid)
            {

                var user = CreateUser();

                user.Voornaam = Input.Voornaam;
                user.GeboorteDatum = Input.Geboortedatum;
                user.Gemeente = Input.Gemeente;
                user.Huisdokter = Input.Huisdokter;
                user.Straat = Input.Straat;
                user.Postcode = Input.Postcode;
                user.Huisnummer = Input.Huisnummer;
                user.Naam = Input.Naam;
                user.TelefoonNummer = Input.PhoneNumber;
                user.PhoneNumber = Input.PhoneNumber;
                user.IsActief=true;
                user.LevelId = 1;
                user.AantalPunten = 0;


                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (!_roleManager.RoleExistsAsync("beheerder").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("beheerder"));
                    await _roleManager.CreateAsync(new IdentityRole("gebruiker"));
                    // multiple role add
                }

                if (result.Succeeded)
                {


                    await _userManager.AddToRoleAsync(user, "gebruiker");

                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }



        private CustomUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<CustomUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(CustomUser)}'. " +
                    $"Ensure that '{nameof(CustomUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        [AllowAnonymous]

        private IUserEmailStore<CustomUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<CustomUser>)_userStore;
        }
    }
}
