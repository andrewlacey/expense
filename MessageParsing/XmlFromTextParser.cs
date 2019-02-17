using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Interfaces;
using Models;


namespace MessageParsing
{
    public class XmlFromTextParser : IMessageParser
    {
        public ExtractedExpenseDetail ExtractExpenseData(string text)
        {
            //setup a list of the tags we are interested in
            var tagsToFind = new List<string>
            {
                Constants.DescriptionTag,
                Constants.CostCentreTag,
                Constants.DateTag,
                Constants.PaymentMethodTag,
                Constants.TotalTag,
                Constants.VendorTag
            };


            //find xml tags in text
            string pattern = "<[^>]*>";
            Regex rgx = new Regex(pattern);

            //get a list of tag names
            var xmlTags = new List<string>();
            foreach (Match match in rgx.Matches(text))
            {
                var tagText = CleanTags(match.Value);
                xmlTags.Add(tagText);
            }

            //initial validation on content
             var validationResult =  ValidateXmlTags(xmlTags, tagsToFind);
            if (!validationResult.IsValid)
            {
                return new ExtractedExpenseDetail()
                {
                    Success = false,
                    ValidationMessage = validationResult.ValidationError
                };
            }


            //now get rid of any closing tags so we just have a list of tags that we are interested in
            xmlTags.RemoveAll(t => t.Contains("/"));

            return ExtractData(xmlTags, text);
        }


        private ValidationResult  ValidateXmlTags(List<string> xmlTags, List<string> tagsToFind)
        {
            if (!xmlTags.Contains(Constants.TotalTag))
            {
                return new ValidationResult {IsValid = false, ValidationError = $"xml tag {Constants.TotalTag} is missing "};
            }

            foreach (var tag in xmlTags)
            {
                if (!tag.Contains("/"))
                {
                    //ignore any tags that we are not interested in
                    if (!tagsToFind.Contains(tag))
                    {
                        continue;
                    }

                    //starting tag make sure there is a closing tag    
                    if (!xmlTags.Contains("/" + tag))
                    {
                        return new ValidationResult { IsValid = false, ValidationError = $"xml tag {tag} is missing closing tag"};
                    }
                }
            }

            return new ValidationResult {IsValid = true};
        }

        private ExtractedExpenseDetail ExtractData(List<string> fieldsToExtract, string text)
        {
            var extractedDetail = new ExtractedExpenseDetail();
            if (fieldsToExtract.Contains(Constants.TotalTag))
            {
                extractedDetail.Total = DoDataExtraction(Constants.TotalTag, text);
            }

            if (fieldsToExtract.Contains(Constants.CostCentreTag))
            {
                extractedDetail.CostCentre = DoDataExtraction(Constants.CostCentreTag, text);
            }

            if (fieldsToExtract.Contains(Constants.PaymentMethodTag))
            {
                extractedDetail.PaymentMethod = DoDataExtraction(Constants.PaymentMethodTag, text);
            }

            if (fieldsToExtract.Contains(Constants.VendorTag))
            {
                extractedDetail.Vendor = DoDataExtraction(Constants.VendorTag, text);
            }

            if (fieldsToExtract.Contains(Constants.DescriptionTag))
            {
                extractedDetail.Description = DoDataExtraction(Constants.DescriptionTag, text);
            }

            if (fieldsToExtract.Contains(Constants.DateTag))
            {
                extractedDetail.Date = DoDataExtraction(Constants.DateTag, text);
            }

            extractedDetail.Success = true;
            return extractedDetail;
        }

        private string DoDataExtraction(string field, string text)
        {
            var startTag = "<" + field + ">";
            var endTag = "</" + field + ">";

            int pFrom = text.IndexOf(startTag, StringComparison.InvariantCulture) + startTag.Length;
            int pTo = text.LastIndexOf(endTag, StringComparison.InvariantCulture);

            string result = text.Substring(pFrom, pTo - pFrom);

            return result;
        }

        private string CleanTags(string text)
        {
            return text.Replace("<", "").Replace(">", "");
        }

    }
}
