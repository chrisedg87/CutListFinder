using System.Collections.Generic;
using CutListFinder.Data;
using CutListFinder.Models;
using Microsoft.AspNetCore.Components;

namespace CutListFinder.Pages;

public class FetchDataBase : ComponentBase
{

  public CutListForm cutListForm = new CutListForm();

  // public int stockLength = 22;

  public List<List<int>> bestCombination;

  public List<List<List<int>>> stock = new List<List<List<int>>>();

  [Inject] WeatherForecastService ForecastService { get; set; }

  protected override async Task OnInitializedAsync()
  {
    //forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
  }

  public void RemoveLength(int index)
  {
    cutListForm.Lengths.RemoveAt(index);
  }

  public void RunPermute()
  {
    bestCombination = null;
    // cutListForm.Lengths.ForEach(e => Console.WriteLine(e));
    var results = Permute(cutListForm.Lengths.ToArray());
    foreach(var result in results)
    {
      try
      {
        var currentCombo = CheckCombination(result, cutListForm.StockLength.Value);
        if (bestCombination == null)
          bestCombination = currentCombo;

        if (bestCombination != null)
        {
          if (currentCombo.Count < bestCombination!.Count)
          {
            bestCombination = currentCombo;
          }
        }
      } catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      
    }
  }

  public void AddLength() 
  {
    cutListForm.Lengths.Add(0);
  }

  public List<List<int>> CheckCombination(IList<int> nums, int stockLen)
  {
    var total = 1;
    var currentTotal = 0;

    var list = new List<List<int>>();
    var currentList = new List<int>();

    foreach(var num in nums)
    {

      if (num > stockLen)
        throw new Exception("Stock not long enough");

      if (currentTotal + num > stockLen)
      {
        // Can't fit
        total++;
        currentTotal = num;
        list.Add(currentList);
        currentList = new List<int>();
      }
      else
      {
        // Can fit 
        currentTotal += num;
      }
      currentList.Add(num);

    }

    if (currentList.Count > 0)
      list.Add(currentList);

    return list;
  }

  public IList<IList<int>> Permute(int[] nums)
  {
    var list = new List<IList<int>>();
    return DoPermute(nums, 0, nums.Length - 1, list);
  }

  public IList<IList<int>> DoPermute(int[] nums, int start, int end, IList<IList<int>> list)
  {
    if (start == end)
    {
      list.Add(new List<int>(nums));
    }
    else
    {
      for (var i = start; i <= end; i++)
      {
        Swap(ref nums[start], ref nums[i]);
        DoPermute(nums, start + 1, end, list);
        Swap(ref nums[start], ref nums[i]);
      }
    }

    return list;
  }

  public void Swap(ref int a, ref int b)
  {
    var temp = a;
    a = b;
    b = temp;
  }
}