package com.teds.erp360.dao;

import java.util.List;

import javax.ejb.Stateless;
import javax.persistence.Query;

import com.teds.erp360.interfaces.dao.IProductoDao;
import com.teds.erp360.model.Compania;
import com.teds.erp360.model.Empresa;
import com.teds.erp360.model.LineaProducto;
import com.teds.erp360.model.Producto;
import com.teds.erp360.util.FacesUtil;
import com.teds.erp360.util.IdGenerator;

/**
 * @author fredyclarosrojas
 *
 */

@Stateless
public class ProductoDao extends DataAccessObject<Producto> implements IProductoDao {
	private IdGenerator generadorCodigo = new IdGenerator();
	public ProductoDao(){
		super(Producto.class);
	}

	public Producto registrar(Producto producto){
		try{
			producto = create(producto);
	        producto.setCodigo(generadorCodigo.generarCodigo("PR-", producto.getId()));
			FacesUtil.infoMessage("Registro Correcto", "Producto "+producto.getNombre());
			return producto;
		}catch(Exception e){
			Throwable t=e.getCause();
			String cause=t.getMessage();
			System.out.println("ERROR"+cause);
			System.out.println("ERROR"+t.getCause());
			if (cause.contains("org.hibernate.exception.ConstraintViolationException: could not execute statement")) {
				FacesUtil.errorMessage("Ya existe un registro igual.");
			}else{
				FacesUtil.errorMessage("Error al registrar");
			}
			//rollbackTransaction();
			return null;
		}
	}

	public boolean modificar(Producto producto){
		try{
			update(producto);
			FacesUtil.infoMessage("Modificación Correcta", "Producto "+producto.getNombre());
			return true;
		}catch(Exception e){
			Throwable t=e.getCause();
			String cause=t.getMessage();
			System.out.println("ERROR"+cause);
			if (cause.contains("org.hibernate.exception.ConstraintViolationException: could not execute statement")) {
				FacesUtil.errorMessage("Ya existe un registro igual.");
			}else{
				FacesUtil.errorMessage("Error al modificar");
			}
			rollbackTransaction();
			return false;
		}
	}

	@Override
	public boolean eliminar(Producto producto){
		try{
			producto.setEstado("RM");
			update(producto);
			FacesUtil.infoMessage("Eliminación Correcta", "Producto "+producto.getNombre());
			return true;
		}catch(Exception e){
			String cause=e.getMessage();
			if (cause.contains("org.hibernate.exception.ConstraintViolationException: could not execute statement")) {
				FacesUtil.errorMessage("Ya existe un registro igual.");
			}else{
				FacesUtil.errorMessage("Error al eliminar");
			}
			rollbackTransaction();
			return false;
		}
	}
/*
	public List<Producto> obtenerProductoOrdenAscPorId(){
		return findAscAllOrderedByParameter("id");
	}
	
	public List<Sucursal> obtenerUsuarioOrdenDescPorId(){
		return findDescAllOrderedByParameter("id");
	}
	
	public List<Sucursal> obtenerPorEmpresa(Empresa empresa){
		return findAllActivosByParameter("empresa", empresa);
	}

	/* (non-Javadoc)
	 * @see com.teds.erp360.interfaces.dao.ISucursalDao#obtenerTodasPorEmpresa(com.teds.erp360.model.Empresa)
	 */



	@Override
	public List<Producto> obtenerTodasPorEmpresa(Empresa empresa) {
		// TODO Auto-generated method stub
		String query = "select em from Producto em where em.empresa.id="+empresa.getId()+" and (em.estado='AC' or em.estado='IN')";
		return executeQueryResulList(query);
	}

	@Override
	public List<Producto> obtenerTodos() {
		// TODO Auto-generated method stub
		String query = "select em from Producto em where (em.estado='AC' or em.estado='IN') ORDER BY em.id ASC";
		return executeQueryResulList(query);
	}
	@Override
	public List<Producto> onComplete(String query) {
		// TODO Auto-generated method stub
		return findAllAndParameterForNameAutoComplete("nombre", query);
	}
    
	@Override
	public List<Producto> devolverProductoOnCompleteCompania(Compania compania, String nombre){
		//String query =  "select NEW com.teds.spaps.model.VentaServicios(p.id, p.descripcion, p.precioVenta,p.grupoServicios) from VentaServicios p where upper(translate(p.descripcion, 'áéíóúÁÉÍÓÚäëïöüÄËÏÖÜñ', 'aeiouAEIOUaeiouAEIOUÑ')) like '%"
		String query =  "select p from Producto p where upper(translate(p.nombre, 'áéíóúÁÉÍÓÚäëïöüÄËÏÖÜñ', 'aeiouAEIOUaeiouAEIOUÑ')) like '%"
				+ nombre + "%'";
		Query q = getEntityManager().createQuery(query);
		return (List<Producto>)q.getResultList();
	}
	
	
	@Override
	public List<Producto> obtenerproductoOrdenAscPorId() {
		// TODO Auto-generated method stub
		return findAscAllOrderedByParameter("id");
	}
	@Override
	public Producto obtenerPorId(Integer id) {
		String query = "select p from Producto p where p.id="+id+" and (p.estado='AC' or p.estado='IN')";
		return executeQuerySingleResult(query);
	}
	
}
