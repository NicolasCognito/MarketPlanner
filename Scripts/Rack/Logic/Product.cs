using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product
{
    //fields
    public ProductData productData;

    //is ghost?
    public bool isGhost;

    //size
    public float width;
    public float height;
    public float depth;
    
    //position
    public float x;
    public float y;

    //rotation
    public Rotation rotation;

    //amount (Z, X, Y)
    //TODO: getter setter
    public Dictionary<Axis, int> amount = new Dictionary<Axis, int>();

    public int totalAmount;

    //constructor (with only productdata)
    public Product(ProductData productData, bool isGhost = false)
    {
        this.productData = productData;
        this.width = productData.base_width;
        this.height = productData.base_height;
        this.depth = productData.base_depth;

        this.rotation = new Rotation();

        //amount
        this.amount.Add(Axis.Z, 1);
        this.amount.Add(Axis.X, 1);
        this.amount.Add(Axis.Y, 1);

        this.totalAmount = 1;
        this.isGhost = isGhost;
    }


    //find actual parameters of product (width, height, depth)
    public void SetActualParameters()
    {
        //find size of product
        width = productData.base_width * amount[Axis.X];
        height = productData.base_height * amount[Axis.Y];
        depth = productData.base_depth * amount[Axis.Z];

        //rotate product
        rotation.ApplyRotation(this);
    }

    //increment amount of product
    public void IncrementAmount(Axis axis, int amount)
    {
        //find actual axis from rotation
        Axis actualAxis = rotation.GetActualAxis(axis);

        //increment amount
        this.amount[actualAxis] += amount;

        //update total amount
        this.totalAmount = this.amount[Axis.Z] * this.amount[Axis.X] * this.amount[Axis.Y];

        //set actual parameters
        SetActualParameters();
    }
     
}


//all possible rotations of 3-dimensional products
public class Rotation
{
    //this class contains all possible rotations of 3-dimensional products

    //fields
    public Dictionary<Axis, Axis> rotations = new Dictionary<Axis, Axis>();

    //constructor
    public Rotation()
    {
        //rotations
        rotations.Add(Axis.X, Axis.X);
        rotations.Add(Axis.Y, Axis.Y);
        rotations.Add(Axis.Z, Axis.Z);
    }

    //rotate product in XY plane
    public void RotateXY(Product product)
    {
        //swap 0 and 1 indexses
        Axis temp_x = rotations[Axis.X];

        rotations[Axis.X] = rotations[Axis.Y];

        rotations[Axis.Y] = temp_x;
    }

    //rotate product in XZ plane
    public void RotateYZ(Product product)
    {
        //swap 0 and 2 indexses
        Axis temp_y = rotations[Axis.Y];

        rotations[Axis.Y] = rotations[Axis.Z];

        rotations[Axis.Z] = temp_y;
    }

    //Get product field by name
    public float GetField(Axis fieldName, Product product)
    {
        if (fieldName == Axis.X)
        {
            return product.width;
        }
        else if (fieldName == Axis.Y)
        {
            return product.height;
        }
        else if (fieldName == Axis.Z)
        {
            return product.depth;
        }
        else
        {
            return 0;
        }
    }

    //set product width, height and depth to actual values according to rotation
    public void ApplyRotation(Product product)
    {
        //initialize temp integers
        float temp_width = GetField(rotations[Axis.X], product);
        float temp_height = GetField(rotations[Axis.Y], product);
        float temp_depth = GetField(rotations[Axis.Z], product);

        //set actual parameters
        product.width = temp_width;
        product.height = temp_height;
        product.depth = temp_depth;
    }

    //get actual axis from axis
    public Axis GetActualAxis(Axis axis)
    {
        return rotations[axis];
    }

}

public enum Axis
{
    X,
    Y,
    Z
}

public static class AxisExtensions
{
    //convert string to axis
    public static Axis ToAxis(this string str)
    {
        if (str == "X")
        {
            return Axis.X;
        }
        else if (str == "Y")
        {
            return Axis.Y;
        }
        else if (str == "Z")
        {
            return Axis.Z;
        }
        else
        {
            throw new System.Exception("Invalid axis");
        }
    }

    //rotation-wise axis to axis
    public static Axis RotationWise(Product product, Axis axis)
    {
        //get actual axis from rotation
        Axis actualAxis = product.rotation.GetActualAxis(axis);

        return actualAxis;
    }
}
