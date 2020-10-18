package com.teds.erp360.model;

import java.io.Serializable;
import java.lang.Integer;
import java.lang.String;
import java.util.Date;
import java.util.List;

import javax.persistence.*;
import javax.validation.constraints.Size;

/**
 * @author fredyclarosrojas
 *
 */
@Entity
@Table(name = "producto", schema = "public", uniqueConstraints = @UniqueConstraint(columnNames = {"nombre"}))
public class Producto implements Serializable {

	private static final long serialVersionUID = 1L;

	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private Integer id;

	@Column(name="codigo",nullable=false )
	private String codigo;

//	@Basic(fetch = FetchType.LAZY)
//    @Lob
    @Column(name = "image")
    private byte[] image;

	@Column(name="codigo_barra",nullable=true )
	private String codigoBarra;

	@Size(max = 100)
	@Column(name = "nombre", nullable = false)
	private String nombre;




	@Column(name = "cantidad_minima", nullable = true)
	private Double cantidadMinima=0.0;

	@Column(name = "cantidad_maxima", nullable = true)
	private Double cantidadMaxima=0.0;


	@Column(name = "precio_Venta", nullable = true)
	private Double precioVenta=0.0;




	@Size(max = 255)
	@Column(name = "descripcion", nullable = false)
	private String descripcion;

	@ManyToOne(fetch = FetchType.EAGER)
	@JoinColumn(name = "id_lineaproducto", nullable = false)
	private LineaProducto lineaProducto;

	@ManyToOne(fetch = FetchType.EAGER)
	@JoinColumn(name = "id_unidadmedida", nullable = false)
	private UnidadMedida unidadMedida;
	
	@OneToMany(mappedBy = "producto", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
	private List<DetalleNotaVenta> detalleNotaVentas;

	@Size(max = 2)// AC , IN , RM
	@Column(name = "estado", nullable = false)
	private String estado;

	@Column(name = "usuario_registro", nullable = false)
	private String usuarioRegistro;


	@Column(name = "fecha_registro", nullable = false)
	private Date fechaRegistro;

	@Column(name = "fecha_modificacion", nullable = true)
	private Date fechaModificacion;

	@Transient
	private Double cantidadAlmacen;

	public Producto() {
		super();
		this.id = 0;
		this.descripcion = "";
		this.unidadMedida=new UnidadMedida();
		this.estado = "AC";
		this.usuarioRegistro = "";
		this.codigo="Generado por el Sistema";
	}


	@Override
	public String toString() {
		return "Producto [id=" + id + ", codigo=" + codigo + ", nombre="
				+ nombre + ", descripcion=" + descripcion + ", lineaProducto="
				+ lineaProducto + ", estado=" + estado + ", usuarioRegistro="
				+ usuarioRegistro + ", fechaRegistro=" + fechaRegistro
				+ ", fechaModificacion=" + fechaModificacion + "]";
	}

	@Override
	public int hashCode() {
		int hash = 0;
		hash += (id != null ? id.hashCode() : 0);
		return hash;
	}

	@Override
	public boolean equals(Object obj) {
		if (obj == null) {
			return false;
		} else {
			if (!(obj instanceof Producto)) {
				return false;
			} else {
				if (((Producto) obj).id == this.id) {
					return true;
				} else {
					return false;
				}
			}
		}
	}

	public Integer getId() {
		return this.id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	public String getDescripcion() {
		return this.descripcion;
	}

	public void setDescripcion(String descripcion) {
		this.descripcion = descripcion;
	}
	public UnidadMedida getUnidadMedida() {
		return unidadMedida;
	}


	public byte[] getImage() {
		return image;
	}


	public void setImage(byte[] image) {
		this.image = image;
	}


	public void setUnidadMedida(UnidadMedida unidadMedida) {
		this.unidadMedida = unidadMedida;
	}	

	public String getEstado() {
		return estado;
	}

	public Date getFechaModificacion() {
		return fechaModificacion;
	}

	public Date getFechaRegistro() {
		return fechaRegistro;
	}

	public String getUsuarioRegistro() {
		return usuarioRegistro;
	}

	public LineaProducto getLineaProducto() {
		return lineaProducto;
	}

	public void setLineaProducto(LineaProducto lineaProducto) {
		this.lineaProducto = lineaProducto;
	}

	public void setUsuarioRegistro(String usuarioRegistro) {
		this.usuarioRegistro = usuarioRegistro;
	}

	public void setEstado(String estado) {
		this.estado = estado;
	}

	public void setFechaModificacion(Date fechaModificacion) {
		this.fechaModificacion = fechaModificacion;
	}

	public void setFechaRegistro(Date fechaRegistro) {
		this.fechaRegistro = fechaRegistro;
	}

	public String getNombre() {
		return nombre;
	}

	public void setNombre(String nombre) {
		this.nombre = nombre;
	}
	public String getCodigo() {
		return codigo;
	}

	public void setCodigo(String codigo) {
		this.codigo = codigo;
	}


	public Double getCantidadMinima() {
		return cantidadMinima;
	}


	public void setCantidadMinima(Double cantidadMinima) {
		this.cantidadMinima = cantidadMinima;
	}


	public Double getPrecioVenta() {
		return precioVenta;
	}


	public void setPrecioVenta(Double precioVenta) {
		this.precioVenta = precioVenta;
	}


	public String getCodigoBarra() {
		return codigoBarra;
	}


	public void setCodigoBarra(String codigoBarra) {
		this.codigoBarra = codigoBarra;
	}



	public Double getCantidadMaxima() {
		return cantidadMaxima;
	}


	public void setCantidadMaxima(Double cantidadMaxima) {
		this.cantidadMaxima = cantidadMaxima;
	}


	public Double getCantidadAlmacen() {
		return cantidadAlmacen;
	}


	public void setCantidadAlmacen(Double cantidadAlmacen) {
		this.cantidadAlmacen = cantidadAlmacen;
	}


	


	public List<DetalleNotaVenta> getDetalleNotaVentas() {
		return detalleNotaVentas;
	}


	public void setDetalleNotaVentas(List<DetalleNotaVenta> detalleNotaVentas) {
		this.detalleNotaVentas = detalleNotaVentas;
	}


}
