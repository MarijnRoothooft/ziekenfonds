SELECT o.Id, b.Naam AS BestemmingNaam, o.Omschrijving, o.Bedrag, o.Datum, o.Foto
FROM Onkosten o
JOIN Bestemmingen b ON o.BestemmingId = b.Id

