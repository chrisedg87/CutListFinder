namespace CutListFinder.Models;

public class CutListForm
{
  public int? StockLength { get; set; }

  public List<int> Lengths { get; set; }

  public CutListForm()
  {
    Lengths = new List<int> { 0, 0, 0, 0, 1};
  }

}