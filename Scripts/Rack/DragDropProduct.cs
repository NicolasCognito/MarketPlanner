using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropProduct : DragDrop
{

    public GhostProduct ghost;

    public ProductMono product;

    public RackMono rack;

    public bool IsDragging = false;

    //coroutine to wait one second
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
        //trigger alarm
        TimerAlarm();
        //restart timer
        StartCoroutine(Timer());
    }

    public override void DragStart()
    {
        //get rack
        rack = product.rack;

        IsDragging = true;

        //set product as ghost
        product.product.isGhost = true;

        //render rack
        rack.Render();

        //start timer
        StartCoroutine(Timer());
    }

    //on drop
    public override void DragEnd()
    {
        //set product as not ghost
        product.product.isGhost = false;
        
        IsDragging = false;

        //stop timer
        StopCoroutine(Timer());
        
        //render rack
        rack.Render();
    }

    private void TimerAlarm()
    {
        int x_coor = (int)product.transform.localPosition.x;
        int index = ClosestIndex(rack, x_coor);
        //if closest index not equal to current ghost index on rack
        if (rack.rackData.products.IndexOf(product.product) != index)
        {
            //remove product from rack
            rack.rackData.products.Remove(product.product);

            //add product to rack at closest index
            rack.rackData.AddProductOnIndex(product.product, index);

            //render rack
            rack.Render();
        }
    }    

    //Closest index on rack (if neigbours are 4 and 5, then closest index is 5)
    public int ClosestIndex(RackMono rack, int x_coordinate)
    {
        int closestIndex = 0;
        int closestDistance = int.MaxValue;

        for (int i = 0; i < rack.rackData.products.Count; i++)
        {
            int distance = (int)Mathf.Abs(rack.rackData.products[i].x - x_coordinate);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

    //on pointer down
    public override void PointerDown()
    {
        //set product as ghost
        product.product.isGhost = true;
    }
}
