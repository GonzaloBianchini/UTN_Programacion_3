﻿using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class ArticuloManager
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista= new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT ARTICULOS.Id AS Id,ARTICULOS.Codigo AS Codigo,ARTICULOS.Nombre AS Nombre,ARTICULOS.Descripcion AS Descripcion,Articulos. Precio AS Precio,MARCAS.Descripcion AS Marca,CATEGORIAS.Descripcion AS Categoria FROM ARTICULOS\r\nINNER JOIN MARCAS ON MARCAS.Id=ARTICULOS.IdMarca\r\nINNER JOIN CATEGORIAS ON CATEGORIAS.Id=ARTICULOS.IdCategoria");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux= new Articulo();
                    aux.id = (int)datos.Lector["Id"];
                    aux.codigo = (string)datos.Lector["Codigo"];
                    aux.nombre = (string)datos.Lector["Nombre"];
                    aux.descripcion = (string)datos.Lector["Descripcion"];
                    aux.precio = (decimal)datos.Lector["Precio"];
                    aux.marca=new Marca();  //instancio el objeto interno para evitar la null reference
                    aux.marca.descripcion =(string) datos.Lector["Marca"];
                    aux.categoria=new Categoria();  ////instancio el objeto interno para evitar la null reference
                    aux.categoria.descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }


                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Articulo artNue)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO ARTICULOS(Codigo, Nombre, Descripcion, Precio,IdMarca,IdCategoria)VALUES('" + artNue.codigo + "','" + artNue.nombre + "','" + artNue.descripcion + "',"+ artNue.precio +",@IdMarca,@IdCategoria)");
                datos.setearParametro("@IdMarca",artNue.marca.id);
                datos.setearParametro("IdCategoria",artNue.categoria.id);
                datos.ejecutarAccion();
            }
            catch (Exception ex) 
            {
                throw ex; 
            }
            finally 
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Articulo artExistente)
        {

        }
    }
}
