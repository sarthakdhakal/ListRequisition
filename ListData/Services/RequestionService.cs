using ListData.Models;
using System.Data;

namespace ListData.Services
{
    public class RequestionService : IRequestionService
    {
        public List<InitialReturn> GetInitials(List<RequistionApproved> values)
        {
          var dataset = values.GroupBy(t => t.itemCode)
              .Select(t => new { itemCode = t.Key, ApprovedQty = t.Sum(u => u.ApprovedQty) }).ToList();
            List<InitialReturn> model = new List<InitialReturn>();
            foreach (var item in dataset)
            {
                InitialReturn initialReturn = new InitialReturn();
                initialReturn.itemCode = item.itemCode;
                initialReturn.ApprovedQty = item.ApprovedQty;
                model.Add(initialReturn);

            }
            return model;
        }

        public List<ReturnList> ReturnLists(List<RequistionApproved> values)
        {
            var dataset = GetInitials(values);
            List<ReturnList> returnList = new List<ReturnList>();
            foreach (var item in dataset)
            {

                var checkValue = purchaseLists().FirstOrDefault(x => x.itemCode == item.itemCode);
                double QtyValue = checkValue.Qty;
                var dataValues = values.Where(i => i.itemCode == item.itemCode)
                .GroupBy(category => category.Division
                       )
                .Select(grouped => new
                {
                    division = grouped.Key,
                    Value = grouped.Sum(u => u.ApprovedQty)
                }
                );
                foreach (var newData in dataValues)
                {
                    double valueSend;

                    if (item.ApprovedQty > checkValue.Qty)
                    {
                        if (item.ApprovedQty == newData.Value)
                        {
                            valueSend = item.ApprovedQty;
                        }
                        else
                        {
                            double datavalues = item.ApprovedQty - newData.Value;
                            double datavalues1 = datavalues / item.ApprovedQty;
                            double itemValues = datavalues1 * QtyValue;
                            valueSend = Math.Round(itemValues);

                        }
                    }
                    else
                    {
                        valueSend = QtyValue;
                    }
                    returnList.Add(
                           new ReturnList
                           {
                               itemCode = item.itemCode,
                               Division = newData.division,
                               Qty = Convert.ToInt32(valueSend)

                           });
                }


            }
            return returnList;
        }
        public List<PurchaseList> purchaseLists()
        {
            List<PurchaseList> purchaseList = new List<PurchaseList>()
            {
                new PurchaseList(){ itemCode= "M001", Qty=5},
                new PurchaseList(){ itemCode= "M002", Qty=10}
            };
            return purchaseList;
        }
    }
}
