using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y; 
    [SerializeField] int z;
    [SerializeField] int ite;
    [SerializeField] GameObject blankCell;
    [SerializeField] GameObject fullCell;
    [SerializeField] Button generateButton;
    [SerializeField] TMP_InputField sizeX_IF;
    [SerializeField] TMP_InputField sizeY_IF;
    [SerializeField] TMP_InputField sizeZ_IF;
    [SerializeField] TMP_InputField iteNumber_IF;
    [SerializeField] Toggle steppedToggle;

    int iteCount = 0;
    int[, ,] cells;
    bool stepped;
    bool generate;

    void Start()
    {
        CreateCells();
        generate = false;
    } 

    /*void Update()
    {
         //Condicion para que se cree el automata celular.
         //Ite se usa para generar n veces las generaciones.
        if(generate == true && iteCount < ite) {
            iteCount ++;
            int[,] nextCells = new int [rows, cols];
            //Se itera sobre todas las celdas para checar sus vecindarios(si en el vecindario de
            // la celda i hay mas de 5 unos, en la sig generacion la celda i va a valer 1).
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < cols; i++)
                {
                    int num = numOfFilledCells(i, j);
                    if(num >= 5)
                    {
                        nextCells[i, j] = 1;
                    }else{
                        nextCells[i, j] = 0;
                    }
                }
            }
            cells = nextCells;
            //Se itera sobre todas las generaciones para saber si ponerle blanck o full.
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if(IsFilled(i, j) == 0){
                        Instantiate(blankCell, new Vector2(i, j), Quaternion.identity);
                    } else if (IsFilled(i, j) == 1)
                    {
                        Instantiate(fullCell, new Vector2(i, j), Quaternion.identity);
                    }
                }
            }
        }   
    } */
     
   //Se crea una grid random de 1 y 0.
    void CreateCells(){
        cells = new int [x, y, z];
        for (int j = 0; j < x; j++)
        {
            for (int i = 0; i < y; i++)
            {
                for (int k = 0; k < z; k++) {
                cells[i, j, k] = Random.Range(0,2);
                 if(cells[i, j, k] == 0) {
                Instantiate(blankCell, new Vector3(i, j, k), Quaternion.identity);
                } else if (cells[i, j, k] == 1)
                {
                    Instantiate(fullCell, new Vector3(i, j, k), Quaternion.identity);
                } 
                } 
            }
        } 
    }

    //Te regresa el numero de unos que hay en i.
   /*  int numOfFilledCells(int x, int y){
        int num = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if(IsFilled(x + i, y + j) == 1){
                    num++;
                }
            }
        }
        return num;
    }

    //Toma los valores de la UI y los guarda en variables.
    public void GenerateCA(){
    rows = int.Parse(sizeX_IF.text);
    cols = int.Parse(sizeY_IF.text);
    stepped = steppedToggle.isOn;
    ite = int.Parse(iteNumber_IF.text);
    generate = true;
    cells = new int[rows, cols];
    CreateCells();
    }

    //Devuelve si una celda esta llena o no.
    public int IsFilled(int x, int y){
    if (x < 0 || x >= cols || y < 0 || y >= rows) {
        return 0;
    } else { 
       // Debug.Log(x + ", " + y + " =" + cells[x, y]);
        return cells[x, y];
    }
    }*/
}
