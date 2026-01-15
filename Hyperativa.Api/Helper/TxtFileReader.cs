using Hyperativa.Api.Models;

namespace Hyperativa.Api.Helper
{
    public static class TxtFileReader
    {
        public static List<CreditCard> ProcessFile(IFormFile file)
        {
            var lines = ReadLines(file);

            if (lines == null)
                throw new Exception("Arquivo vazio ou inválido.");

            var header = HeaderProcess(lines.Value.Header);            
            var body = BodyProcess(lines.Value.Body);            
            var footer = FooterProcess(lines.Value.Footer);            

            var lot = new Lot
            {
                Name = header.Name,
                NumberOfRecords = IntTryParse(header.NumberOfRecords),
                LotIssueDate = DateTimeTryParse(header.Date),
                LotCode = header.Lot,
            };

            var creditCards = body.Select(b => new CreditCard
            (
                b.FullCardNumber,
                lot,
                b.LotNumber,
                b.Identifier
            )).ToList();

            return creditCards;
        }

        private static DateTime DateTimeTryParse(string dateString)
        {
            DateTime parsedDate;
            if (DateTime.TryParseExact(dateString, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
            }
            else
            {
                throw new FormatException("Data em formato inválido.");
            }
        }

        private static int IntTryParse(string intString)
        {
            int parsedInt;
            if (int.TryParse(intString, out parsedInt))
            {
                return parsedInt;
            }
            else
            {
                throw new FormatException("Número em formato inválido.");
            }
        }

        private static (string Header, string Footer, List<string> Body)? ReadLines(IFormFile file)
        {
            List<string> bodyLines = [];
            string header = string.Empty;
            string footer = string.Empty;

            using var reader = new StreamReader(file.OpenReadStream());

            string currentLine = reader.ReadLine();

            if (currentLine == null)
                return null; // arquivo vazio

            string nextLine = reader.ReadLine();

            header = currentLine;

            currentLine = nextLine;
            nextLine = reader.ReadLine();

            while (nextLine != null && !string.IsNullOrWhiteSpace(nextLine))
            {
                bodyLines.Add(currentLine);

                currentLine = nextLine;
                nextLine = reader.ReadLine();
            }

            footer = currentLine;

            return (header, footer, bodyLines);
        }

        private class BodyLine
        {
            public string Identifier { get; set; } = string.Empty;
            public string LotNumber { get; set; } = string.Empty;
            public string FullCardNumber { get; set; } = string.Empty;

        }

        private class HeaderLine
        {
            public string Name { get; set; } = string.Empty;
            public string Date { get; set; } = string.Empty;
            public string Lot { get; set; } = string.Empty;
            public string NumberOfRecords { get; set; } = string.Empty;
        }

        private class FooterLine
        {
            public string Lot { get; set; } = string.Empty;
            public string RecordCount { get; set; } = string.Empty;
        }

        private static List<BodyLine> BodyProcess(List<string> lines)
        {
            // [01-01]IDENTIFICADOR DA LINHA   [02-07]NUMERAÇÃO NO LOTE   [08-26]NÚMERO DE CARTAO COMPLETO

            var bodyLines = new List<BodyLine>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Precisa ter pelo menos 7 caracteres fixos
                if (line.Length < 7)
                    continue;

                var maxLength = Math.Min(line.Length, 26);
                BodyLine bodyLine = new BodyLine
                {
                    Identifier = line.Substring(0, 1).Trim(),
                    LotNumber = line.Substring(1, 6).Trim(),
                    FullCardNumber = line[7..maxLength].Trim()
                };
                
                bodyLines.Add(bodyLine);
            }

            return bodyLines;
        }

        private static HeaderLine HeaderProcess(string lines)
        {
            //[01-29]NOME   [30-37]DATA   [38-45]LOTE   [46-51]QTD DE REGISTROS

            var headerLine = new HeaderLine
            {
                Name = lines.Substring(0, 29).Trim(),
                Date = lines.Substring(29, 8).Trim(),
                Lot = lines.Substring(37, 8).Trim(),
                NumberOfRecords = lines.Substring(45, 6).Trim()
            };

            return headerLine;
        }

        private static FooterLine FooterProcess(string lines)
        {
            // [01-08]LOTE   [09-14]QTD DE REGISTROS

            var footerLine = new FooterLine
            {
                Lot = lines.Substring(0, 8).Trim(),
                RecordCount = lines.Substring(8, 6).Trim()
            };

            return footerLine;
        }
    }
}