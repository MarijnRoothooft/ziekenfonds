$(document).ready(function () {
    // Reken bedragen af naar 2 decimalen wanneer de gebruiker het veld verlaat
    $("input[data-val-number]").on("blur", function () {
        let value = $(this).val();

        // Vervang komma door punt en probeer de waarde te parsen
        value = value.replace(",", ".");
        let parsedValue = parseFloat(value);

        if (!isNaN(parsedValue)) {
            // Rond af naar 2 decimalen en converteer terug naar een komma-notatie
            $(this).val(parsedValue.toFixed(2).replace(".", ","));
        } else {
            // Als de waarde ongeldig is, leeg het veld
            $(this).val("");
        }
    });
});
