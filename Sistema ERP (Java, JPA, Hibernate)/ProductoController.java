package com.teds.erp360.controller;

import java.io.IOException;
import java.io.InputStream;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ViewScoped;
import javax.faces.context.FacesContext;
import javax.faces.event.AjaxBehaviorEvent;
import javax.inject.Inject;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.Part;

import org.apache.commons.io.IOUtils;
import org.omnifaces.util.Faces;
import org.omnifaces.util.Utils;
import org.primefaces.component.fileupload.FileUpload;
import org.primefaces.event.FileUploadEvent;
import org.primefaces.event.SelectEvent;
import org.primefaces.model.DefaultStreamedContent;
import org.primefaces.model.StreamedContent;
import org.primefaces.model.UploadedFile;








import com.teds.erp360.interfaces.dao.ILineaProductoDao;
import com.teds.erp360.interfaces.dao.IOrdenIngresoDao;
import com.teds.erp360.interfaces.dao.IOrdenSalidaDao;
import com.teds.erp360.interfaces.dao.IProductoDao;
import com.teds.erp360.interfaces.dao.IUnidadMedidaDao;
import com.teds.erp360.model.DetalleOrdenIngreso;
import com.teds.erp360.model.DetalleOrdenSalida;
import com.teds.erp360.model.FamiliaProducto;
import com.teds.erp360.model.LineaProducto;
import com.teds.erp360.model.Producto;
import com.teds.erp360.model.UnidadMedida;
import com.teds.erp360.session.SessionMain;
import com.teds.erp360.util.FacesUtil;



/**
 * @author fredyclarosrojas
 *
 */
@ManagedBean(name = "productoController")
@ViewScoped
public class ProductoController implements Serializable {

	private static final long serialVersionUID = 844192813700641693L;

	// DAO
	private @Inject IProductoDao productoDao;
	private @Inject ILineaProductoDao lineaProductoDao;
	private @Inject IUnidadMedidaDao unidadMedidaDao;
	private @Inject SessionMain sessionMain;
	private @Inject IOrdenIngresoDao ordenIngresoDAo;
	private @Inject IOrdenSalidaDao ordenSalidaDao;

	// OBJECT
	private Producto producto;
	private LineaProducto lineaProducto;
	private UnidadMedida unidadMedida;
	 private UploadedFile image;
	 private StreamedContent imagem;
	 
	// LIST
	private List<Producto> listaProducto;
	private List<LineaProducto> listaLineaProducto;
	private List<UnidadMedida> listaUnidadMedida;



	//VAR
	private String currentPage = "/pages/inventario/param/producto/list.xhtml";

	//ESTADOS
	private boolean nuevo=true;
	private boolean registrar=false;
	private boolean seleccionado=false;
	private boolean imageModified;
	public ProductoController() {
	}

	@PostConstruct
	public void initNew() {
		defaultValues();
		
		
	}

	public byte[] obtenerVector() throws IOException{
		return Utils.toByteArray(Faces.getResourceAsStream("/resources/img/placeholder.png"));
	}
	
	private void defaultValues() {
		nuevo = true;
		registrar = false;
		seleccionado = false;
		producto = new Producto();
		//FacesContext.getCurrentInstance().getExternalContext().getRealPath("").getBytes();
		//producto.setImage();
		lineaProducto=new LineaProducto();
		unidadMedida=new UnidadMedida();
		listaProducto = productoDao.obtenerTodos();
	//	listaLineaProducto= lineaProductoDao.obtenerLineaProductoOrdenAscPorId();
	//	listaLineaProducto= lineaProductoDao.obtenerLineaaProductoOrdenadoPorNombrePorEmpresa(sessionMain.getEmpresaLogin());
		listaLineaProducto=lineaProductoDao.obtenerTodos();
		listaUnidadMedida=unidadMedidaDao.obtenerTodasPorEmpresa(sessionMain.getEmpresaLogin());
	}

	
	//ACTION
	public void onRowSelect(SelectEvent event) {
		nuevo = false;
		seleccionado = true;
		registrar = false;
		lineaProducto=producto.getLineaProducto();
		unidadMedida=producto.getUnidadMedida();
		 imageModified = false;
		currentPage = "/pages/inventario/param/producto/edit.xhtml";
	}

	public void actionNuevo() throws IOException {
		nuevo = false;
		seleccionado = false;
		registrar = true;
		producto=new Producto();
		producto.setImage(obtenerVector());
		//agregado recien
		
		//defaultValues();
		
		currentPage = "/pages/inventario/param/producto/edit.xhtml";
		
	}

	public List<LineaProducto> onCompleteLineaProducto(String query) {
		List<LineaProducto> listaLineaProductoConsultados=new ArrayList<>();
		for(LineaProducto fp : listaLineaProducto){
			if(fp.getNombre().toLowerCase().startsWith(query)){
				listaLineaProductoConsultados.add(fp);
			}
		}
		return listaLineaProductoConsultados;
	}

	public List<UnidadMedida> onCompleteUnidadMedida(String query) {
		List<UnidadMedida> listaUnidadMedidaConsultados=new ArrayList<>();
		for(UnidadMedida fp : listaUnidadMedida){			
			if(fp.getNombre().toLowerCase().startsWith(query)){
				listaUnidadMedidaConsultados.add(fp);
			}
		}
		return listaUnidadMedidaConsultados;
	}

	public void actionCancelar() {
		nuevo = true;
		seleccionado = false;
		registrar = false;
		lineaProducto=new LineaProducto();
		unidadMedida=new UnidadMedida();
		producto = new Producto();
		
		currentPage = "/pages/inventario/param/producto/list.xhtml";
	}

	//PRODCESO

	public void registrarProducto() {
		if (lineaProducto.getId().intValue() == 0
				|| lineaProducto == null) {
			FacesUtil.infoMessage("VALIDACION", "Seleccione Una Linea De Producto");
			return;
		}
		if (unidadMedida.getId().intValue() == 0
				|| unidadMedida == null) {
			FacesUtil.infoMessage("VALIDACION", "Seleccione Una Unidad De Medida");
			return;
		}
		producto.setFechaRegistro(new Date());
		producto.setUsuarioRegistro(sessionMain.getUsuarioLogin().getLogin());
		producto.setLineaProducto(lineaProducto);
		producto.setUnidadMedida(unidadMedida);
		producto.setNombre(producto.getNombre().trim());
		producto.setDescripcion(producto.getDescripcion().trim());
		Producto r = productoDao.registrar(producto);
		System.out.println(lineaProducto);
		System.out.println(unidadMedida);
		System.out.println(r);
		if (r != null) {
			defaultValues();
			currentPage = "/pages/inventario/param/producto/list.xhtml";
		}
	}

	public void modificarProducto() {
		if (lineaProducto.getId().intValue() == 0
				|| lineaProducto == null) {
			FacesUtil.infoMessage("VALIDACION", "Seleccione Una Linea De Producto");
			return;
		}
		if (unidadMedida.getId().intValue() == 0
				|| unidadMedida == null) {
			FacesUtil.infoMessage("VALIDACION", "Seleccione Una Unidad De Medida");
			return;
		}
		producto.setFechaModificacion(new Date());
		producto.setLineaProducto(lineaProducto);
		producto.setUnidadMedida(unidadMedida);
		producto.setNombre(producto.getNombre().trim());
		producto.setDescripcion(producto.getDescripcion().trim());
		boolean sw = productoDao.modificar(producto);
		if (sw) {
			defaultValues();
			currentPage = "/pages/inventario/param/producto/list.xhtml";
		}
	}
    
	public void verReporte(){
		
		nuevo = false;
		seleccionado=false;
	
		registrar = false;
	
	    List<DetalleOrdenIngreso> listaIngresos=ordenIngresoDAo.listaIngresosPorProducto(producto);
	    List<DetalleOrdenSalida> listaSalidas=ordenSalidaDao.listaSalidasPorProducto(producto);
		try {
			
			for (DetalleOrdenIngreso detalleOrdenIngreso : listaIngresos) {
				detalleOrdenIngreso.setMotivo(detalleOrdenIngreso.getOrdenIngreso().getMotivoIngreso().getLabel());
				//detalleOrdenIngreso.set
			}
			for (DetalleOrdenSalida detalleOrdenIngreso : listaSalidas) {
				detalleOrdenIngreso.setMotivo(detalleOrdenIngreso.getOrdenSalida().getMotivoSalida().getLabel());
				//detalleOrdenIngreso.set
			}

			Map<String, Object> map = new HashMap<String, Object>();
			map.put("listaEntrada",listaIngresos);
			map.put("listaSalida",listaSalidas);
			map.put("usuario", sessionMain.getUsuarioLogin().getLogin());
			
			map.put("SUBREPORT_DIR", FacesContext.getCurrentInstance().getExternalContext().getRealPath("/resources/report/inventario/")+"/");
			//map.put("MotivoIngreso", ordenIngreso.getMotivoIngreso().getLabel());
			String reportPath = FacesContext.getCurrentInstance().getExternalContext().getRealPath("/resources/report/inventario/Kardex.jasper");
			HttpServletRequest request = (HttpServletRequest) FacesContext.getCurrentInstance().getExternalContext().getRequest();       
			request.getSession().setAttribute("map", map);
		    request.getSession().setAttribute("ruta", reportPath);
		   // request.getSession().setAttribute("lista", listaAlmacenProducto);
		} catch (Exception e) {
			System.out.println("Fallo en "+e.toString());
		}
		currentPage = "/pages/inventario/param/producto/reporte.xhtml";
	}
	
	public void eliminarProducto() {
		if (productoDao.eliminar(producto)) {
			defaultValues();
			currentPage = "/pages/inventario/param/producto/list.xhtml";
		} 
	}

	//GET AND SETTER

	public boolean isSeleccionado() {
		return seleccionado;
	}

	public void setSeleccionado(boolean seleccionado) {
		this.seleccionado = seleccionado;
	}

	public UnidadMedida getUnidadMedida() {
		return unidadMedida;
	}

	public void setUnidadMedida(UnidadMedida unidadMedida) {
		this.unidadMedida = unidadMedida;
	}

	public LineaProducto getLineaProducto() {
		return lineaProducto;
	}

	public void setLineaProducto(LineaProducto lineaProducto) {
		this.lineaProducto = lineaProducto;
	}

	public boolean isRegistrar() {
		return registrar;
	}

	public void setRegistrar(boolean registrar) {
		this.registrar = registrar;
	}
	public List<UnidadMedida> getListaUnidadMedida() {
		return listaUnidadMedida;
	}

	public void setListaUnidadMedida(List<UnidadMedida> listaUnidadMedida) {
		this.listaUnidadMedida = listaUnidadMedida;
	}

	public List<LineaProducto> getListaLineaProducto() {
		return listaLineaProducto;
	}

	public void setListaLineaProducto(List<LineaProducto> listaLineaProducto) {
		this.listaLineaProducto = listaLineaProducto;
	}

	public Producto getProducto() {
		return producto;
	}

	public void setProducto(Producto producto) {
		this.producto = producto;
	}

	public List<Producto> getListaProducto() {
		return listaProducto;
	}

	public void setListaProducto(List<Producto> listaProducto) {
		this.listaProducto = listaProducto;
	}

	public String getPage() {
		return currentPage;
	}

	public void setPage(String currentPage) {
		this.currentPage = currentPage;
	}

	public boolean isNuevo() {
		return nuevo;
	}

	public void setNuevo(boolean nuevo) {
		this.nuevo = nuevo;
	}

	

	

	public UploadedFile getImage() {
		return image;
	}

	public void setImage(UploadedFile image) {
		this.image=image;
	}
	
	public void fileUploadListener(FileUploadEvent e){
		// Get uploaded file from the FileUploadEvent
		this.image = e.getFile();
		
		if (image != null) {
            try {
                InputStream input = image.getInputstream();
                imagem = new DefaultStreamedContent(input);
                producto.setImage(IOUtils.toByteArray(input));
                System.out.println("ENTRO EN SETER IMAGEN");
            } catch (IOException ex) {
                Logger.getLogger(ProductoController.class.getName()).log(Level.SEVERE, null, ex);
            }
        } else if (image == null && producto.getImage() != null && imageModified == true) {
            producto.setImage(null);
            System.out.println("ENTRO EN SETER IMAGEN NULL");
        }
		System.out.println("ENTRO EN IMAGEN NULL");
	}

	public boolean isImageModified() {
		return imageModified;
	}

	public void setImageModified(boolean imageModified) {
		this.imageModified = imageModified;
	}

	public StreamedContent getImagem() {
		return imagem;
	}

	public void setImagem(StreamedContent imagem) {
		this.imagem = imagem;
	}

}
