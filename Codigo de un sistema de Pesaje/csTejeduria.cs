using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geshil.BI
{
	public class csTejeduria
	{
		public static csEstado Estado { get; set; }
		public csMaquinaTejeduria Maquina { get; set; }

		static csTejeduria()
		{
			Estado = new csEstado();
		}

		public static List<csMaquinaTejeduria> GetAllMaquinas()
		{
			Estado.Iniciar();
            List<csMaquinaTejeduria> maquinas = new List<csMaquinaTejeduria>();

			try { 

				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					maquinas = (from a in db.TBL_TEJE_MAQUINAS
					 select new csMaquinaTejeduria
					 {
						 TEJE_MAQUINA_DESCRP = a.TEJE_MAQUINA_DESCRP,
						 TEJE_MAQUINA_ID = a.TEJE_MAQUINA_ID

					 }).ToList();
				}

			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al obtener máquinas");
			}

            return maquinas;
        }

		public static List<csMovimientos> GetAllRecepciones()
		{
			Estado.Iniciar();
			List<csMovimientos> movs = new List<csMovimientos>();
			try
			{
				using (DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					movs = (from x in db.TBL_MOVCAB
							where x.MOVCAB_CODEMP == Seguridad.BI.csUsuario.Empresa &&
									x.MOVCAB_CODMOV == csEstados.TipoMovimiento.RECTJ
							select new csMovimientos
							{
								MOVCAB_CODEMP = x.MOVCAB_CODEMP,
								MOVCAB_CODMOV = x.MOVCAB_CODMOV,
								MOVCAB_NROMOV = x.MOVCAB_NROMOV,
								MOVCAB_PLANTA = x.MOVCAB_PLANTA,
								MOVCAB_NETO = x.MOVCAB_NETO,
								MOVCAB_BRUTO = x.MOVCAB_BRUTO,
								MOVCAB_TARA = x.MOVCAB_TARA,
								MOVCAB_ARTCOD = x.MOVCAB_ARTCOD,
								MOVCAB_TIPPRO = x.MOVCAB_TIPPRO,
								MOVCAB_FECALT = x.MOVCAB_FECALT,
								MOVCAB_ESTADO = x.MOVCAB_ESTADO,
								MOVCAB_INTER_NROFOR = x.MOVCAB_INTER_NROFOR,
								MOVCAB_CANTBULBAL = x.MOVCAB_CANTBULBAL

							}).OrderByDescending(x => x.MOVCAB_NROMOV).Take(1000).ToList();

					foreach (var mov in movs)
					{
						mov.MOVCAB_DESCRP = csProducto.Get(mov.MOVCAB_TIPPRO, mov.MOVCAB_ARTCOD).STMPDH_DESCRP;
					}

				}
			}
			catch (Exception ex)
			{
				Estado.CapturarError(ex, "Error al recepciones", csEstado.eRelevancia.Grave);
			}

			return movs;
		}

		public static List<csMovimientos> GetAllPesajes()
		{
			Estado.Iniciar();
			List<csMovimientos> movs = new List<csMovimientos>();
			try
			{
				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					movs = (from x in db.TBL_MOVCAB
							where x.MOVCAB_CODEMP == Seguridad.BI.csUsuario.Empresa &&
									x.MOVCAB_CODMOV == csEstados.TipoMovimiento.PROBTJ
							select new csMovimientos
							{
								MOVCAB_CODEMP = x.MOVCAB_CODEMP,
								MOVCAB_CODMOV = x.MOVCAB_CODMOV,
								MOVCAB_NROMOV = x.MOVCAB_NROMOV,
								MOVCAB_PLANTA = x.MOVCAB_PLANTA,
								MOVCAB_NETO = x.MOVCAB_NETO,
								MOVCAB_BRUTO = x.MOVCAB_BRUTO,
								MOVCAB_TARA = x.MOVCAB_TARA,
								MOVCAB_PROG_NRO = (int)x.MOVCAB_PROG_NRO,
								MOVCAB_ARTCOD = x.MOVCAB_ARTCOD,
								MOVCAB_TIPPRO = x.MOVCAB_TIPPRO,
								MOVCAB_FECALT = x.MOVCAB_FECALT,
								MOVCAB_ESTADO = x.MOVCAB_ESTADO,
								MOVCAB_INTER_NROFOR = x.MOVCAB_INTER_NROFOR,
								MOVCAB_CANTBULBAL = x.MOVCAB_CANTBULBAL

							}).OrderByDescending(x => x.MOVCAB_NROMOV).Take(1000).ToList();

					foreach(var mov in movs)
					{
						mov.MOVCAB_DESCRP = csProducto.Get(mov.MOVCAB_TIPPRO, mov.MOVCAB_ARTCOD).STMPDH_DESCRP;
					}

				}
			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al obtener pesajes", csEstado.eRelevancia.Grave);
			}

			return movs;
        }

		public static List<csMovimientos> GetAllArmadoPallets()
		{
			Estado.Iniciar();
			List<csMovimientos> movs;

			using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
			{
				movs = (from a in db.TBL_MOVCAB
						where a.MOVCAB_CODEMP == Seguridad.BI.csUsuario.Empresa &&
								a.MOVCAB_CODMOV == csEstados.TipoMovimiento.ARPLTJ &&
								a.MOVCAB_ESTADO != csEstados.EstadosMovimiento.BORRADO
						select new csMovimientos
						{
							MOVCAB_CODEMP = a.MOVCAB_CODEMP,
							MOVCAB_PLANTA = a.MOVCAB_PLANTA,
							MOVCAB_CODMOV = a.MOVCAB_CODMOV,
							MOVCAB_NROMOV = a.MOVCAB_NROMOV,
							MOVCAB_ARTCOD = a.MOVCAB_ARTCOD,
							MOVCAB_BRUTO = a.MOVCAB_BRUTO,
							MOVCAB_CANTBULBAL = a.MOVCAB_CANTBULBAL,
							MOVCAB_CANTBULCPTEORI = a.MOVCAB_CANTBULCPTEORI,
							MOVCAB_CANTKGBAL = a.MOVCAB_CANTKGBAL,
							MOVCAB_CANTKGCPTEORI = a.MOVCAB_CANTKGCPTEORI,
							MOVCAB_CODEMP_DES = a.MOVCAB_CODEMP_DES,
							MOVCAB_CTAORI = a.MOVCAB_CTAORI,
							MOVCAB_FECALT = a.MOVCAB_FECALT,
							MOVCAB_ESTADO = a.MOVCAB_ESTADO,
							MOVCAB_FECMOV = a.MOVCAB_FECMOV,
							MOVCAB_NETO = a.MOVCAB_NETO,
							MOVCAB_NROCPTEORI = a.MOVCAB_NROCPTEORI,
							MOVCAB_TIPPRO = a.MOVCAB_TIPPRO,
							MOVCAB_NOTROS = a.MOVCAB_NOTROS
						}

                        ).OrderByDescending(y => y.MOVCAB_NROMOV).Take(1000).ToList();


				foreach(var mov in movs)
				{
					mov.MOVCAB_DESCRP = csProducto.Get(mov.MOVCAB_TIPPRO, mov.MOVCAB_ARTCOD).STMPDH_DESCRP;
                }
			}

			return movs;
		}

		public static void CancelarRecepcion(csMovimientos movimiento)
		{
			Estado.Iniciar();
			try
			{
				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					var items = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == movimiento.MOVCAB_CODEMP &&
														  x.MOVITEMS_CODMOV == movimiento.MOVCAB_CODMOV &&
														  x.MOVITEMS_NROMOV == movimiento.MOVCAB_NROMOV);
					foreach(var item in items)
					{
						var itemBorrar = db.TBL_STRMVK.Where(x => x.STRMVK_TIPPRO == item.MOVITEMS_TIPPRO &&
						 										x.STRMVK_ARTCOD == item.MOVITEMS_ARTCOD &&
																x.STRMVK_NSERIE == item.MOVITEMS_NSERIE &&
																x.STRMVK_DEPOSI == item.MOVITEMS_DEPOSI &&
																x.STRMVK_SECTOR == item.MOVITEMS_SECTOR).FirstOrDefault();
						if (itemBorrar != null)
						{
							db.TBL_STRMVK.Remove(itemBorrar);
						}

			//			itemBorrar.STRMVK_ESTADO = csEstados.EstadosMovimiento.BORRADO;

					}

					var mov = db.TBL_MOVCAB.Single(x => x.MOVCAB_CODEMP == movimiento.MOVCAB_CODEMP &&
													x.MOVCAB_CODMOV == movimiento.MOVCAB_CODMOV &&
													x.MOVCAB_NROMOV == movimiento.MOVCAB_NROMOV);

					mov.MOVCAB_ESTADO = csEstados.EstadosMovimiento.BORRADO;

					db.SaveChanges();

				}

			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al cancelar recepcion", csEstado.eRelevancia.Grave);
			}
		}

		public static void CompletarRecepcion(csMovimientos movimiento)
		{
			Estado.Iniciar();
			try
			{
				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					var mov  = db.TBL_MOVCAB.Single(x => x.MOVCAB_CODEMP == movimiento.MOVCAB_CODEMP &&
											x.MOVCAB_CODMOV == movimiento.MOVCAB_CODMOV &&
											x.MOVCAB_NROMOV == movimiento.MOVCAB_NROMOV);

					mov.MOVCAB_ESTADO = csEstados.EstadosMovimiento.TERMINADO;

					var items = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == mov.MOVCAB_CODEMP &&
															x.MOVITEMS_CODMOV == mov.MOVCAB_CODMOV &&
															x.MOVITEMS_NROMOV == mov.MOVCAB_NROMOV);

					int cantidad = items.Count();
					decimal suma = items.Sum(x => (decimal)x.MOVITEMS_NETO);

					mov.MOVCAB_CANTBULBAL = cantidad;
					mov.MOVCAB_CANTBULCPTEORI = cantidad;
					mov.MOVCAB_CANTKGBAL = suma;
					mov.MOVCAB_CANTKGCPTEORI = suma;
					mov.MOVCAB_NETO = suma;
					mov.MOVCAB_BRUTO = suma;
					mov.MOVCAB_TARA = 0;

					db.SaveChanges();
				}

			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al completar recepcion", csEstado.eRelevancia.Grave);
			}
		}

		public static csMovimientos GetRecepcionPendiente()
		{
			Estado.Iniciar();
			csMovimientos mov;

			try
			{
				using (DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					mov = (from m in db.TBL_MOVCAB
						   where m.MOVCAB_CODEMP == Seguridad.BI.csUsuario.Empresa &&
							   m.MOVCAB_CODMOV == csEstados.TipoMovimiento.RECTJ &&
							   m.MOVCAB_ESTADO == csEstados.EstadosMovimiento.HABILITADO
						   select new csMovimientos
						   {
							   MOVCAB_CODEMP = m.MOVCAB_CODEMP,
							   MOVCAB_CODMOV = m.MOVCAB_CODMOV,
							   MOVCAB_NROMOV = m.MOVCAB_NROMOV,
							   MOVCAB_PLANTA = m.MOVCAB_PLANTA,
							   MOVCAB_TIPPRO = m.MOVCAB_TIPPRO,
							   MOVCAB_ARTCOD = m.MOVCAB_ARTCOD,
							   MOVCAB_ESTADO = m.MOVCAB_ESTADO,
							   MOVCAB_NOTROS = m.MOVCAB_NOTROS

						   }).FirstOrDefault();

					if (mov != null)
					{
						mov.Items = (from itm in db.TBL_MOVITEMS
									 where itm.MOVITEMS_CODEMP == mov.MOVCAB_CODEMP &&
											itm.MOVITEMS_CODMOV == mov.MOVCAB_CODMOV &&
											itm.MOVITEMS_NROMOV == mov.MOVCAB_NROMOV &&
											itm.MOVITEMS_PRINCIPAL != true
									 select new csItems
									 {
										 MOVITEMS_CODEMP = itm.MOVITEMS_CODEMP,
										 MOVITEMS_CODMOV = itm.MOVITEMS_CODMOV,
										 MOVITEMS_PLANTA = itm.MOVITEMS_PLANTA,
										 MOVITEMS_NROITM = itm.MOVITEMS_NROITM,
										 MOVITEMS_NROMOV = itm.MOVITEMS_NROMOV,
										 MOVITEMS_TIPPRO = itm.MOVITEMS_TIPPRO,
										 MOVITEMS_ARTCOD = itm.MOVITEMS_ARTCOD,
										 MOVITEMS_NSERIE = (int)itm.MOVITEMS_NSERIE,
										 MOVITEMS_CANTID = itm.MOVITEMS_CANTID,
										 MOVITEMS_CNTSEC = itm.MOVITEMS_CNTSEC,
										 MOVITEMS_NETO = (decimal)itm.MOVITEMS_NETO,
										 MOVITEMS_BRUTO = (decimal)itm.MOVITEMS_BRUTO,
										 MOVITEMS_TARA = (decimal)itm.MOVITEMS_TARA

									 }).ToList();

						var itmPrincipal = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == mov.MOVCAB_CODEMP &&
																	x.MOVITEMS_CODEMP == mov.MOVCAB_CODEMP &&
																	x.MOVITEMS_NROMOV == mov.MOVCAB_NROMOV &&
																	x.MOVITEMS_PRINCIPAL == true).FirstOrDefault();

						if(itmPrincipal != null) { 
							mov.MOVCAB_TIPPRO = itmPrincipal.MOVITEMS_TIPPRO;
							mov.MOVCAB_ARTCOD = itmPrincipal.MOVITEMS_ARTCOD;
							mov.MOVCAB_NOTROS = itmPrincipal.MOVITEMS_NOTROS;
						}
					}
				}

				return mov;

			}
			catch (Exception ex)
			{
				Estado.CapturarError(ex, "Error al obtener armado pendiente");
			}

			return null;
		}


		public static csMovimientos GetArmadoPendiente()
		{
			Estado.Iniciar();
			csMovimientos mov;

			try { 
				using (DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					mov = (from m in db.TBL_MOVCAB
							where m.MOVCAB_CODEMP == Seguridad.BI.csUsuario.Empresa &&
								m.MOVCAB_CODMOV == csEstados.TipoMovimiento.ARPLTJ &&
								m.MOVCAB_ESTADO == csEstados.EstadosMovimiento.HABILITADO
							select new csMovimientos
							{
								MOVCAB_CODEMP = m.MOVCAB_CODEMP,
								MOVCAB_CODMOV = m.MOVCAB_CODMOV,
								MOVCAB_NROMOV = m.MOVCAB_NROMOV,
								MOVCAB_PLANTA = m.MOVCAB_PLANTA,
								MOVCAB_TIPPRO = m.MOVCAB_TIPPRO,
								MOVCAB_ARTCOD = m.MOVCAB_ARTCOD,
								MOVCAB_ESTADO = m.MOVCAB_ESTADO

							}).FirstOrDefault();

					if(mov != null)
					{
						mov.Items = (from itm in db.TBL_MOVITEMS
									 where itm.MOVITEMS_CODEMP == mov.MOVCAB_CODEMP &&
											itm.MOVITEMS_CODMOV == mov.MOVCAB_CODMOV &&
											itm.MOVITEMS_NROMOV == mov.MOVCAB_NROMOV &&
											itm.MOVITEMS_PRINCIPAL != true
									 select new csItems
									 {
										 MOVITEMS_CODEMP = itm.MOVITEMS_CODEMP,
										 MOVITEMS_CODMOV = itm.MOVITEMS_CODMOV,
										 MOVITEMS_PLANTA = itm.MOVITEMS_PLANTA,
										 MOVITEMS_NROITM = itm.MOVITEMS_NROITM,
										 MOVITEMS_NROMOV = itm.MOVITEMS_NROMOV,
										 MOVITEMS_TIPPRO = itm.MOVITEMS_TIPPRO,
										 MOVITEMS_ARTCOD = itm.MOVITEMS_ARTCOD,
										 MOVITEMS_NSERIE = (int)itm.MOVITEMS_NSERIE,
										 MOVITEMS_CANTID = itm.MOVITEMS_CANTID,
										 MOVITEMS_CNTSEC = itm.MOVITEMS_CNTSEC,
										 MOVITEMS_NETO = (decimal)itm.MOVITEMS_NETO,
										 MOVITEMS_BRUTO = (decimal)itm.MOVITEMS_BRUTO,
										 MOVITEMS_TARA = (decimal)itm.MOVITEMS_TARA

									 }).ToList();

						var itmPrincipal = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == mov.MOVCAB_CODEMP &&
																	x.MOVITEMS_CODEMP == mov.MOVCAB_CODEMP &&
																	x.MOVITEMS_NROMOV == mov.MOVCAB_NROMOV &&
																	x.MOVITEMS_PRINCIPAL == true).First();

						mov.MOVCAB_TIPPRO = itmPrincipal.MOVITEMS_TIPPRO;
						mov.MOVCAB_ARTCOD = itmPrincipal.MOVITEMS_ARTCOD;
						mov.MOVCAB_NOTROS = itmPrincipal.MOVITEMS_NOTROS;
																	
					}
				}

				return mov;

			}
			catch (Exception ex)
			{
				Estado.CapturarError(ex, "Error al obtener armado pendiente");
			}

			return null;
		}

		public static IList<csItems> GetAllDetalleArmadoPallet(csMovimientos cabecera)
		{
			using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
			{
				return (from x in db.TBL_MOVITEMS
						where x.MOVITEMS_CODEMP == cabecera.MOVCAB_CODEMP &&
								x.MOVITEMS_CODMOV == cabecera.MOVCAB_CODMOV &&
								x.MOVITEMS_NROMOV == cabecera.MOVCAB_NROMOV &&
								x.MOVITEMS_PRINCIPAL == false
						select new csItems
						{
							MOVITEMS_CODEMP = x.MOVITEMS_CODEMP,
							MOVITEMS_CODMOV = x.MOVITEMS_CODMOV,
							MOVITEMS_PLANTA = x.MOVITEMS_PLANTA,
							MOVITEMS_NROMOV = x.MOVITEMS_NROMOV,
							MOVITEMS_TIPPRO = x.MOVITEMS_TIPPRO,
							MOVITEMS_DESCRP = x.MOVITEMS_DESCRP,
							MOVITEMS_ARTCOD = x.MOVITEMS_ARTCOD,
							MOVITEMS_NETO = (decimal)x.MOVITEMS_NETO,
							MOVITEMS_CANTID = x.MOVITEMS_CANTID,
							MOVITEMS_BRUTO = (decimal)x.MOVITEMS_BRUTO,
							MOVITEMS_CNTSEC = x.MOVITEMS_CNTSEC,
							MOVITEMS_ENVASE = x.MOVITEMS_ENVASE,
							MOVITEMS_NOTROS = x.MOVITEMS_NOTROS,
							MOVITEMS_NSERIE = (int)x.MOVITEMS_NSERIE,
							MOVITEMS_NROITM = x.MOVITEMS_NROITM,
							MOVITEMS_TARA = (decimal)x.MOVITEMS_TARA
						}).ToList();
			}
		}

		public static IList<csItems> GetAllDetallePesaje(csMovimientos cabecera)
		{

			using (DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
			{

				//MOVITEMS_FALLA_ID = (x.MOVITEMS_FALLA_ID == null ? string.Empty : x.MOVITEMS_FALLA_ID.ToString())

				var itemsDB = (from x in db.TBL_MOVITEMS
							   where x.MOVITEMS_CODEMP == cabecera.MOVCAB_CODEMP &&
									   x.MOVITEMS_CODMOV == cabecera.MOVCAB_CODMOV &&
									   x.MOVITEMS_NROMOV == cabecera.MOVCAB_NROMOV
							   select x);

				var items = new List<csItems>();

				foreach(var x in itemsDB)
				{
					var itm = new csItems
					{
						MOVITEMS_CODEMP = x.MOVITEMS_CODEMP,
						MOVITEMS_CODMOV = x.MOVITEMS_CODMOV,
						MOVITEMS_PLANTA = x.MOVITEMS_PLANTA,
						MOVITEMS_NROMOV = x.MOVITEMS_NROMOV,
						MOVITEMS_TIPPRO = x.MOVITEMS_TIPPRO,
						MOVITEMS_DESCRP = x.MOVITEMS_DESCRP,
						MOVITEMS_ARTCOD = x.MOVITEMS_ARTCOD,
						MOVITEMS_NETO = (decimal)x.MOVITEMS_NETO,
						MOVITEMS_CANTID = x.MOVITEMS_CANTID,
						MOVITEMS_BRUTO = (decimal)x.MOVITEMS_BRUTO,
						MOVITEMS_CNTSEC = x.MOVITEMS_CNTSEC,
						MOVITEMS_ENVASE = x.MOVITEMS_ENVASE,
						MOVITEMS_NOTROS = x.MOVITEMS_NOTROS,
						MOVITEMS_NSERIE = (int)x.MOVITEMS_NSERIE,
						MOVITEMS_NROITM = x.MOVITEMS_NROITM,
						MOVITEMS_TARA = (decimal)x.MOVITEMS_TARA
					};

					itm.MOVITEMS_FALLA_ID = x.MOVITEMS_FALLA_ID.ToString();

					items.Add(itm);
				}
				return items;
			}
		}
		public static IList<csItems> GetAllDetalleRecepcion(csMovimientos cabecera)
		{

			using (DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
			{

				//MOVITEMS_FALLA_ID = (x.MOVITEMS_FALLA_ID == null ? string.Empty : x.MOVITEMS_FALLA_ID.ToString())

				var itemsDB = (from x in db.TBL_MOVITEMS
							   where x.MOVITEMS_CODEMP == cabecera.MOVCAB_CODEMP &&
									   x.MOVITEMS_CODMOV == cabecera.MOVCAB_CODMOV &&
									   x.MOVITEMS_NROMOV == cabecera.MOVCAB_NROMOV
							   select x);

				var items = new List<csItems>();

				foreach (var x in itemsDB)
				{
					var itm = new csItems
					{
						MOVITEMS_CODEMP = x.MOVITEMS_CODEMP,
						MOVITEMS_CODMOV = x.MOVITEMS_CODMOV,
						MOVITEMS_PLANTA = x.MOVITEMS_PLANTA,
						MOVITEMS_NROMOV = x.MOVITEMS_NROMOV,
						MOVITEMS_TIPPRO = x.MOVITEMS_TIPPRO,
						MOVITEMS_DESCRP = x.MOVITEMS_DESCRP,
						MOVITEMS_ARTCOD = x.MOVITEMS_ARTCOD,
						MOVITEMS_NETO = (decimal)x.MOVITEMS_NETO,
						MOVITEMS_CANTID = x.MOVITEMS_CANTID,
						MOVITEMS_BRUTO = (decimal)x.MOVITEMS_BRUTO,
						MOVITEMS_CNTSEC = x.MOVITEMS_CNTSEC,
						MOVITEMS_ENVASE = x.MOVITEMS_ENVASE,
						MOVITEMS_NOTROS = x.MOVITEMS_NOTROS,
						MOVITEMS_NSERIE = (int)x.MOVITEMS_NSERIE,
						MOVITEMS_NROITM = x.MOVITEMS_NROITM,
						MOVITEMS_TARA = (decimal)x.MOVITEMS_TARA
					};

					itm.MOVITEMS_FALLA_ID = x.MOVITEMS_FALLA_ID.ToString();

					items.Add(itm);
				}
				return items;
			}
		}

		public static csMovimientos GrabarRecepcion(csItems item)
		{
			Estado.Iniciar();

			csMovimientos mov = new csMovimientos();

			try
			{

				using (DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					mov.MOVCAB_CODEMP = Seguridad.BI.csUsuario.Empresa;
					mov.MOVCAB_CODMOV = csEstados.TipoMovimiento.RECTJ;

					try
					{
						mov.MOVCAB_NROMOV = db.TBL_MOVCAB.Where(x => x.MOVCAB_CODEMP == Seguridad.BI.csUsuario.Empresa && x.MOVCAB_CODMOV == csEstados.TipoMovimiento.RECTJ).Max(x => x.MOVCAB_NROMOV) + 1;
					}
					catch (Exception)
					{
						mov.MOVCAB_NROMOV = 1;
					}

					mov.MOVCAB_NROCPTEORI = string.Empty;
					mov.MOVCAB_CTAORI = string.Empty;
					mov.MOVCAB_PLANTA = Seguridad.BI.csPlantas.MiPlanta.PC_PLANTA_COD;
					mov.MOVCAB_TIPOCPTEORI = "REM";
					mov.MOVCAB_CANTKGCPTEORI = 0;
					mov.MOVCAB_CANTBULCPTEORI = 0;
					mov.MOVCAB_CANTKGBAL = 0;
					mov.MOVCAB_CANTBULBAL = 0;
					mov.MOVCAB_FECALT = DateTime.Now;
					mov.MOVCAB_USERID = Seguridad.BI.csUsuario.Login;
					mov.MOVCAB_ESTADO  = csEstados.EstadosMovimiento.HABILITADO;
					mov.MOVCAB_TIPPRO = item.MOVITEMS_TIPPRO;
					mov.MOVCAB_ARTCOD = item.MOVITEMS_ARTCOD;
					mov.MOVCAB_NOTROS = item.MOVITEMS_NOTROS;
					mov.MOVCAB_EXCEPCION = string.Empty;
					mov.MOVCAB_PESAJE_MANUAL = true;
					mov.MOVCAB_TARA_UNIDAD = 0;
					mov.MOVCAB_BRUTO = item.MOVITEMS_BRUTO;
					mov.MOVCAB_NETO = item.MOVITEMS_NETO;
					mov.MOVCAB_TARA_UNIDAD = item.MOVITEMS_BRUTO;
					mov.MOVCAB_OBSERVACIONES = string.Empty;
					mov.MOVCAB_EXCEPCION = string.Empty;
					mov.MOVCAB_NOTROS = item.MOVITEMS_NOTROS;
					mov.Items = new List<csItems>();

					DAL.TBL_MOVCAB _csMovimientos = new DAL.TBL_MOVCAB();

					_csMovimientos.MOVCAB_CODEMP = mov.MOVCAB_CODEMP;
                    _csMovimientos.MOVCAB_CODMOV = mov.MOVCAB_CODMOV;
					_csMovimientos.MOVCAB_NROMOV = mov.MOVCAB_NROMOV;

					_csMovimientos.MOVCAB_NROCPTEORI = mov.MOVCAB_NROCPTEORI;
					_csMovimientos.MOVCAB_CTAORI = mov.MOVCAB_CTAORI;
					_csMovimientos.MOVCAB_PLANTA = mov.MOVCAB_PLANTA;
					_csMovimientos.MOVCAB_TIPOCPTEORI = mov.MOVCAB_TIPOCPTEORI;
					_csMovimientos.MOVCAB_CANTKGCPTEORI = mov.MOVCAB_CANTKGCPTEORI;
					_csMovimientos.MOVCAB_CANTBULCPTEORI = mov.MOVCAB_CANTBULCPTEORI;
					_csMovimientos.MOVCAB_CANTKGBAL = mov.MOVCAB_CANTKGBAL;
					_csMovimientos.MOVCAB_CANTBULBAL = mov.MOVCAB_CANTBULBAL;
					_csMovimientos.MOVCAB_FECALT = mov.MOVCAB_FECALT;
					_csMovimientos.MOVCAB_USERID = mov.MOVCAB_USERID;
					_csMovimientos.MOVCAB_ESTADO = mov.MOVCAB_ESTADO;
					_csMovimientos.MOVCAB_TIPPRO = mov.MOVCAB_TIPPRO;
					_csMovimientos.MOVCAB_ARTCOD = mov.MOVCAB_ARTCOD;
					_csMovimientos.MOVCAB_EXCEPCION = mov.MOVCAB_EXCEPCION;
					_csMovimientos.MOVCAB_PESAJE_MANUAL = mov.MOVCAB_PESAJE_MANUAL;
					_csMovimientos.MOVCAB_TARA_UNIDAD = mov.MOVCAB_TARA_UNIDAD;
					_csMovimientos.MOVCAB_BRUTO = mov.MOVCAB_BRUTO;
					_csMovimientos.MOVCAB_NETO = mov.MOVCAB_NETO;
					_csMovimientos.MOVCAB_TARA = mov.MOVCAB_TARA;
					_csMovimientos.MOVCAB_OBSERVACIONES = mov.MOVCAB_OBSERVACIONES;
					_csMovimientos.MOVCAB_NOTROS = mov.MOVCAB_NOTROS;
					
					db.TBL_MOVCAB.Add(_csMovimientos);
					db.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				Estado.CapturarError(ex, "Error al grabar romaneo de tejeduría", csEstado.eRelevancia.Grave);
			}

			return mov;
		}

		public static void GrabarRomaneo(csProducto Producto, csClientes Cliente, decimal value, int nroRemito)
		{
			Estado.Iniciar();

			try { 

				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					DAL.TBL_MOVCAB _csMovimientos = new DAL.TBL_MOVCAB();
					_csMovimientos.MOVCAB_CODEMP = Seguridad.BI.csUsuario.Empresa;
					_csMovimientos.MOVCAB_CODMOV = csEstados.TipoMovimiento.ROMTJ;

					try { 
						_csMovimientos.MOVCAB_NROMOV = db.TBL_MOVCAB.Where(x=>x.MOVCAB_CODEMP == Seguridad.BI.csUsuario.Empresa && x.MOVCAB_CODMOV == csEstados.TipoMovimiento.ROMTJ).Max(x=>x.MOVCAB_NROMOV) + 1;
					}catch(Exception)
					{
						_csMovimientos.MOVCAB_NROMOV = 1;
                    }
					_csMovimientos.MOVCAB_NROCPTEORI = nroRemito.ToString();

					if(Cliente != null)
					{
						_csMovimientos.MOVCAB_CTAORI = Cliente.CLIENTES_NROCTA;
					}

					_csMovimientos.MOVCAB_PLANTA = Seguridad.BI.csPlantas.MiPlanta.PC_PLANTA_COD;
					_csMovimientos.MOVCAB_TIPOCPTEORI = "REM";
					_csMovimientos.MOVCAB_CANTKGCPTEORI = 0;
					_csMovimientos.MOVCAB_CANTBULCPTEORI = 0;
					_csMovimientos.MOVCAB_CANTKGBAL = 0;
					_csMovimientos.MOVCAB_CANTBULBAL = 0;
					_csMovimientos.MOVCAB_FECALT = DateTime.Now;
					_csMovimientos.MOVCAB_USERID = Seguridad.BI.csUsuario.Login;
					_csMovimientos.MOVCAB_ESTADO = csEstados.EstadosMovimiento.HABILITADO;
					_csMovimientos.MOVCAB_TIPPRO = Producto.STMPDH_TIPPRO;
					_csMovimientos.MOVCAB_ARTCOD = Producto.STMPDH_ARTCOD;
					_csMovimientos.MOVCAB_EXCEPCION = string.Empty;
					_csMovimientos.MOVCAB_PESAJE_MANUAL = true;
					_csMovimientos.MOVCAB_TARA_UNIDAD = 0;
					_csMovimientos.MOVCAB_BRUTO = value;
					_csMovimientos.MOVCAB_NETO = 0;
					_csMovimientos.MOVCAB_OBSERVACIONES = "Habilitado";
					db.TBL_MOVCAB.Add(_csMovimientos);
					db.SaveChanges();
				}

			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al grabar romaneo de tejeduría", csEstado.eRelevancia.Grave);
			}

		}

		public static IList<csItems> GetAllDetalleRomaneo(csMovimientos cabecera)
		{
			Estado.Iniciar();
			IList<csItems> items = new List<csItems>();
			try
			{
				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					items = (from det in db.TBL_MOVITEMS
					 where  det.MOVITEMS_CODEMP == cabecera.MOVCAB_CODEMP &&
							det.MOVITEMS_CODMOV == cabecera.MOVCAB_CODMOV &&
							det.MOVITEMS_NROMOV == cabecera.MOVCAB_NROMOV
					 select new csItems
					 {
						 MOVITEMS_CODEMP = det.MOVITEMS_CODEMP,
						 MOVITEMS_CODMOV = det.MOVITEMS_CODMOV,
						 MOVITEMS_NROMOV = det.MOVITEMS_NROMOV,
						 MOVITEMS_PLANTA = det.MOVITEMS_PLANTA,
						 MOVITEMS_TIPPRO = det.MOVITEMS_TIPPRO,
						 MOVITEMS_ARTCOD = det.MOVITEMS_ARTCOD,
						 MOVITEMS_DESCRP = det.MOVITEMS_DESCRP,
						 MOVITEMS_NETO = (decimal)det.MOVITEMS_NETO,
						 MOVITEMS_BRUTO = (decimal)det.MOVITEMS_BRUTO,
						 MOVITEMS_TARA = (decimal)det.MOVITEMS_TARA,
						 MOVITEMS_CANTID = det.MOVITEMS_CANTID,
						 MOVITEMS_CNTSEC = det.MOVITEMS_CNTSEC,
						 MOVITEMS_NSERIE = (int)det.MOVITEMS_NSERIE
					 }).ToList();
				}
			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al obtener detalle de romaneo de tejeduría", csEstado.eRelevancia.Grave);
			}

			return items;
		}

		/// <summary>
		/// Trackeo de pallet
		/// </summary>
		/// <param name="codemp">Código de empresa</param>
		/// <param name="nromov">Nro de movimiento del trackeo (ROMTJ) </param>
		/// <param name="nserie">Número de serie del pallet (que es el número de movimiento)</param>
		/// <param name="userid">usuario</param>
		/// <returns>Lista de items</returns>
		public static List<csItems> GrabarPalletRomaneo(string codemp, int nromov, int nserie, string userid)
		{
			List<csItems> items = new List<csItems>();
			Estado.Iniciar();
			try
			{
				using (var db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					using (var tran = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
					{

						var movRomaneo = db.TBL_MOVCAB.Single(x => x.MOVCAB_CODEMP == codemp &&
																   x.MOVCAB_CODMOV == Geshil.BI.csEstados.TipoMovimiento.ROMTJ &&
																   x.MOVCAB_NROMOV == nromov &&
																   x.MOVCAB_ESTADO != csEstados.EstadosMovimiento.BORRADO );

						var movPallet = db.TBL_MOVCAB.Single(x => x.MOVCAB_CODEMP == codemp &&
																  x.MOVCAB_CODMOV == Geshil.BI.csEstados.TipoMovimiento.ARPLTJ &&
																  x.MOVCAB_NROMOV == nserie &&
																  x.MOVCAB_ESTADO == csEstados.EstadosMovimiento.TERMINADO);

						List<DAL.TBL_MOVITEMS> listaItemsAgregar = new List<DAL.TBL_MOVITEMS>();

						int nroItm = 1;

						var itemsAux = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == movRomaneo.MOVCAB_CODEMP &&
																x.MOVITEMS_CODMOV == movRomaneo.MOVCAB_CODMOV &&
																x.MOVITEMS_NROMOV == movRomaneo.MOVCAB_NROMOV);

						if (itemsAux != null && itemsAux.Count() > 0)
						{
							nroItm = itemsAux.Max(y => y.MOVITEMS_NROITM) + 1;
						}


						foreach (var item in movPallet.TBL_MOVITEMS.Where(x=> !(bool)x.MOVITEMS_PRINCIPAL))
						{
							
							var pie = db.TBL_STRMVK.Where(x =>   x.STRMVK_TIPPRO == item.MOVITEMS_TIPPRO &&
																 x.STRMVK_ARTCOD == item.MOVITEMS_ARTCOD &&
																 x.STRMVK_NSERIE == item.MOVITEMS_NSERIE).FirstOrDefault();

							if (pie == null)
							{
								throw new Exceptions.InvalidDataException("La pieza " + pie.STRMVK_NSERIE + " no corresponde con el artículo, o no existe.");
							}

							if (pie.STRMVK_NETO <= 0)
							{
								throw new Exceptions.InvalidDataException("La pieza " + pie.STRMVK_NSERIE + " no tiene stock");
							}

							if (movRomaneo.TBL_MOVITEMS.Where(x =>  x.MOVITEMS_TIPPRO == pie.STRMVK_TIPPRO &&
																	x.MOVITEMS_ARTCOD == pie.STRMVK_ARTCOD &&
																	x.MOVITEMS_NSERIE == pie.STRMVK_NSERIE).Count() > 0)
							{
								throw new Exceptions.InvalidDataException("La pieza " + pie.STRMVK_NSERIE + " ya fué trakeada.");
							}

							if (pie.STRMVK_ESTADO != Geshil.BI.csEstados.EstadosMovimiento.TERMINADO)
							{
								throw new Exceptions.InvalidDataException("La pieza " + pie.STRMVK_NSERIE + " no está disponible.");
							}

							movRomaneo.MOVCAB_NETO += pie.STRMVK_NETO;

							decimal tolerancia = GetPorcentajeToleranciaKgsTrackeo();
							decimal kgsLimite = ((decimal)movRomaneo.MOVCAB_BRUTO + ((decimal)movRomaneo.MOVCAB_BRUTO * (tolerancia / 100)));

							if (movRomaneo.MOVCAB_NETO > kgsLimite)
							{
								throw new Exceptions.InvalidDataException("Se alcanzó el límite de trackeo.");
							}

							pie.STRMVK_ESTADO = Geshil.BI.csEstados.EstadosMovimiento.RESERVA;

							string descrp = db.TBL_STMPDH.Single(x => x.STMPDH_TIPPRO == pie.STRMVK_TIPPRO && x.STMPDH_ARTCOD == pie.STRMVK_ARTCOD).STMPDH_DESCRP;

							int nroItmActual = nroItm++;

							listaItemsAgregar.Add(new DAL.TBL_MOVITEMS
							{
								MOVITEMS_ARTCOD = pie.STRMVK_ARTCOD,
								MOVITEMS_NSERIE = pie.STRMVK_NSERIE,
								MOVITEMS_BRUTO = pie.STRMVK_BRUTO,
								MOVITEMS_CANTID = pie.STRMVK_NETO,
								MOVITEMS_CNTSEC = pie.STRMVK_STKSEC,
								MOVITEMS_CODEMP = movRomaneo.MOVCAB_CODEMP,
								MOVITEMS_CODMOV = movRomaneo.MOVCAB_CODMOV,
								MOVITEMS_DEPOSI = (string.IsNullOrEmpty(pie.STRMVK_DEPOSI) ? String.Empty : pie.STRMVK_DEPOSI),
								MOVITEMS_DEBAJA = pie.STRMVK_DEBAJA,
								MOVITEMS_DESCRP = descrp,
								MOVITEMS_ENVASE = pie.STRMVK_ENVASE,
								MOVITEMS_FECALT = DateTime.Now,
								MOVITEMS_NATRIB = pie.STRMVK_NATRIB,
								MOVITEMS_NETO = pie.STRMVK_NETO,
								MOVITEMS_NOTROS = pie.STRMVK_NOTROS,
								MOVITEMS_NROMOV = movRomaneo.MOVCAB_NROMOV,
								MOVITEMS_OBSERV = String.Empty,
								MOVITEMS_PESAJE_MANUAL = false,
								MOVITEMS_USERID = userid,
								MOVITEMS_UNIMED = "K",
								MOVITEMS_PLANTA = item.MOVITEMS_PLANTA,
								MOVITEMS_TIPPRO = item.MOVITEMS_TIPPRO,
								MOVITEMS_TRANSFERIDO = false,
								MOVITEMS_TIPOEMBAL = String.Empty,
								MOVITEMS_SECTOR = (string.IsNullOrEmpty(pie.STRMVK_SECTOR) ? String.Empty : pie.STRMVK_SECTOR),
								MOVITEMS_TARA = pie.STRMVK_TARA,
								MOVITEMS_NROITM = nroItmActual
							});

							items.Add(new csItems
							{
								MOVITEMS_ARTCOD = pie.STRMVK_ARTCOD,
								MOVITEMS_NSERIE = (int)pie.STRMVK_NSERIE,
								MOVITEMS_TIPPRO = pie.STRMVK_TIPPRO,
								MOVITEMS_BRUTO = (decimal)pie.STRMVK_BRUTO,
								MOVITEMS_NETO = (decimal)pie.STRMVK_NETO,
								MOVITEMS_NOTROS = pie.STRMVK_NOTROS,
								MOVITEMS_CODEMP = movRomaneo.MOVCAB_CODEMP,
								MOVITEMS_CODMOV = movRomaneo.MOVCAB_CODMOV,
								MOVITEMS_NROMOV = movRomaneo.MOVCAB_NROMOV,
								MOVITEMS_NROITM = nroItmActual,
                                MOVITEMS_ENVASE = pie.STRMVK_ENVASE,
								MOVITEMS_TARA = (decimal)pie.STRMVK_TARA,
								MOVITEMS_PLANTA = item.MOVITEMS_PLANTA
							});
						}

						foreach (var itmAgregar in listaItemsAgregar)
						{
							db.TBL_MOVITEMS.Add(itmAgregar);
						}

						db.SaveChanges();
						tran.Complete();
					}
				}
			}
			catch (Exceptions.InvalidDataException ex)
			{
				Estado.CapturarError(ex, "Error de validación.", csEstado.eRelevancia.Normal);
			}
			catch (Exception ex)
			{
				Estado.CapturarError(ex, "Error al grabar item de romaneo", csEstado.eRelevancia.Grave);
			}

			return items;
		}

		public static csItems GrabarItemRomaneo(csItems item)
		{
			Estado.Iniciar();
			try {

				using (var db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					using (var tran = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
					{

						var mov = db.TBL_MOVCAB.Where(y => y.MOVCAB_CODEMP == item.MOVITEMS_CODEMP &&
															y.MOVCAB_CODMOV == item.MOVITEMS_CODMOV &&
															y.MOVCAB_NROMOV == item.MOVITEMS_NROMOV).FirstOrDefault();

						if (mov == null)
						{
							throw new NullReferenceException("El movimiento no existe");
						}

						if (mov.MOVCAB_ESTADO != Geshil.BI.csEstados.EstadosMovimiento.HABILITADO)
						{
							throw new Exceptions.InvalidDataException("El romaneo ya no se encuentra habilitado");
						}

						item.MOVITEMS_TIPPRO = mov.MOVCAB_TIPPRO;
						item.MOVITEMS_ARTCOD = mov.MOVCAB_ARTCOD;

						var pie = db.TBL_STRMVK.Where(x => x.STRMVK_TIPPRO == item.MOVITEMS_TIPPRO &&
															 x.STRMVK_ARTCOD == item.MOVITEMS_ARTCOD &&
															 x.STRMVK_NSERIE == item.MOVITEMS_NSERIE).FirstOrDefault();

						if (pie == null)
						{
							throw new Exceptions.InvalidDataException("La pieza " + pie.STRMVK_NSERIE + " no corresponde con el artículo, o no existe.");
						}

						if (pie.STRMVK_NETO <= 0)
						{
							throw new Exceptions.InvalidDataException("La pieza " + pie.STRMVK_NSERIE + " no tiene stock");
						}

						if (pie.STRMVK_ESTADO != Geshil.BI.csEstados.EstadosMovimiento.TERMINADO)
						{
							throw new Exceptions.InvalidDataException("La pieza " + pie.STRMVK_NSERIE + " no está disponible.");
						}

						mov.MOVCAB_NETO += pie.STRMVK_NETO;

						decimal tolerancia = GetPorcentajeToleranciaKgsTrackeo();
						decimal kgsLimite = ((decimal)mov.MOVCAB_BRUTO + ((decimal)mov.MOVCAB_BRUTO * (tolerancia / 100)));

						if (mov.MOVCAB_NETO > kgsLimite)
						{
							throw new Exceptions.InvalidDataException("Se alcanzó el límite de trackeo.");
						}

						pie.STRMVK_ESTADO = Geshil.BI.csEstados.EstadosMovimiento.RESERVA;

						string descrp = db.TBL_STMPDH.Single(x => x.STMPDH_TIPPRO == pie.STRMVK_TIPPRO && x.STMPDH_ARTCOD == pie.STRMVK_ARTCOD).STMPDH_DESCRP;

						int nroItm = 1;

						try
						{
							nroItm = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == item.MOVITEMS_CODEMP && x.MOVITEMS_CODMOV == item.MOVITEMS_CODMOV && x.MOVITEMS_NROMOV == item.MOVITEMS_NROMOV).Max(y => y.MOVITEMS_NROITM) + 1;
						}
						catch (Exception)
						{

						}

						db.TBL_MOVITEMS.Add(new DAL.TBL_MOVITEMS
						{
							MOVITEMS_ARTCOD = pie.STRMVK_ARTCOD,
							MOVITEMS_NSERIE = pie.STRMVK_NSERIE,
							MOVITEMS_BRUTO = pie.STRMVK_BRUTO,
							MOVITEMS_CANTID = pie.STRMVK_NETO,
							MOVITEMS_CNTSEC = pie.STRMVK_STKSEC,
							MOVITEMS_CODEMP = item.MOVITEMS_CODEMP,
							MOVITEMS_CODMOV = item.MOVITEMS_CODMOV,
							MOVITEMS_DEPOSI = (string.IsNullOrEmpty(pie.STRMVK_DEPOSI) ? String.Empty : pie.STRMVK_DEPOSI),
							MOVITEMS_DEBAJA = pie.STRMVK_DEBAJA,
							MOVITEMS_DESCRP = descrp,
							MOVITEMS_ENVASE = pie.STRMVK_ENVASE,
							MOVITEMS_FECALT = DateTime.Now,
							MOVITEMS_NATRIB = pie.STRMVK_NATRIB,
							MOVITEMS_NETO = pie.STRMVK_NETO,
							MOVITEMS_NOTROS = pie.STRMVK_NOTROS,
							MOVITEMS_NROMOV = item.MOVITEMS_NROMOV,
							MOVITEMS_OBSERV = String.Empty,
							MOVITEMS_PESAJE_MANUAL = false,
							MOVITEMS_USERID = item.MOVITEMS_USERID,
							MOVITEMS_UNIMED = "K",
							MOVITEMS_PLANTA = item.MOVITEMS_PLANTA,
							MOVITEMS_TIPPRO = item.MOVITEMS_TIPPRO,
							MOVITEMS_TRANSFERIDO = false,
							MOVITEMS_TIPOEMBAL = String.Empty,
							MOVITEMS_SECTOR = (string.IsNullOrEmpty(pie.STRMVK_SECTOR) ? String.Empty : pie.STRMVK_SECTOR),
							MOVITEMS_TARA = pie.STRMVK_TARA,
							MOVITEMS_NROITM = nroItm
						});

						db.SaveChanges();
						tran.Complete();

						item = new csItems
						{
							MOVITEMS_ARTCOD = pie.STRMVK_ARTCOD,
							MOVITEMS_NSERIE = (int)pie.STRMVK_NSERIE,
							MOVITEMS_TIPPRO = pie.STRMVK_TIPPRO,
							MOVITEMS_BRUTO = (decimal)pie.STRMVK_BRUTO,
							MOVITEMS_NETO = (decimal)pie.STRMVK_NETO,
							MOVITEMS_NOTROS = pie.STRMVK_NOTROS,
							MOVITEMS_CODEMP = item.MOVITEMS_CODEMP,
							MOVITEMS_CODMOV = item.MOVITEMS_CODMOV,
							MOVITEMS_NROMOV = item.MOVITEMS_NROMOV,
							MOVITEMS_NROITM = item.MOVITEMS_NROITM,
							MOVITEMS_ENVASE = pie.STRMVK_ENVASE,
							MOVITEMS_TARA = (decimal)pie.STRMVK_TARA,
							MOVITEMS_PLANTA = item.MOVITEMS_PLANTA
						};

					}
				}

			}catch(Exceptions.InvalidDataException ex) {

				Estado.CapturarError(ex, "Error de validación.", csEstado.eRelevancia.Normal);

			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al grabar item de romaneo", csEstado.eRelevancia.Grave);
			}

			return item;
		}

		private static decimal GetPorcentajeToleranciaKgsTrackeo()
		{
			return decimal.Parse(Seguridad.BI.csParametros.Parametros.Single(x => x.PARAMETROS_DESCRP == "% Tolerancia kgs trackeo" && x.PARAMETROS_HABILITADO).PARAMETROS_VALOR);
		}

		public static List<csMovimientos> GetAllRomaneos(string empresa, bool recuperaItems = false)
		{
			Estado.Iniciar();
			List<csMovimientos> movimientos = new List<csMovimientos>();
			try
			{
				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{

					var tiempo = DateTime.Now.AddDays(-7);
					movimientos = (from mov in db.TBL_MOVCAB
								   where mov.MOVCAB_CODEMP == empresa &&
										 mov.MOVCAB_CODMOV == csEstados.TipoMovimiento.ROMTJ &&
										 //mov.MOVCAB_ESTADO != csEstados.EstadosMovimiento.BORRADO &&
										 mov.MOVCAB_FECALT >= tiempo
								   select new csMovimientos
								   {
									   MOVCAB_CODEMP = mov.MOVCAB_CODEMP,
									   MOVCAB_PLANTA = mov.MOVCAB_PLANTA,
									   MOVCAB_NROCPTEORI = mov.MOVCAB_NROCPTEORI,
									   MOVCAB_CTAORI = mov.MOVCAB_CTAORI,
									   MOVCAB_CODMOV = mov.MOVCAB_CODMOV,
									   MOVCAB_NROMOV = mov.MOVCAB_NROMOV,
									   MOVCAB_ARTCOD = mov.MOVCAB_ARTCOD,
									   MOVCAB_TIPPRO = mov.MOVCAB_TIPPRO,
									   MOVCAB_NETO = mov.MOVCAB_NETO,
									   MOVCAB_BRUTO = mov.MOVCAB_BRUTO,
									   MOVCAB_CANTBULBAL = mov.MOVCAB_CANTBULBAL,
									   MOVCAB_ESTADO = mov.MOVCAB_ESTADO,
									   MOVCAB_OBSERVACIONES = mov.MOVCAB_OBSERVACIONES,
									   MOVCAB_FECALT = mov.MOVCAB_FECALT,
									   MOVCAB_INTER_NROFOR = mov.MOVCAB_INTER_NROFOR

								   }).OrderByDescending(x=>x.MOVCAB_NROMOV).ToList();

					foreach(var mov in movimientos)
					{
						try { 
							mov.MOVCAB_DESCRP = csProducto.Get(mov.MOVCAB_TIPPRO, mov.MOVCAB_ARTCOD).STMPDH_DESCRP;
							
							if (!string.IsNullOrEmpty(mov.MOVCAB_CTAORI)) {

								mov.CLIENTES_RAZSOC = csClientes.Get(mov.MOVCAB_CTAORI).CLIENTES_RAZSOC;
                            }

							mov.MOVCAB_CANTBULBAL = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == mov.MOVCAB_CODEMP &&
																			 x.MOVITEMS_CODMOV == mov.MOVCAB_CODMOV &&
																			 x.MOVITEMS_NROMOV == mov.MOVCAB_NROMOV).Count();

							if (recuperaItems)
							{
								mov.Items = (from itm in db.TBL_MOVITEMS
											 where  itm.MOVITEMS_CODEMP == mov.MOVCAB_CODEMP &&
													itm.MOVITEMS_CODMOV == mov.MOVCAB_CODMOV &&
													itm.MOVITEMS_NROMOV == mov.MOVCAB_NROMOV
											 select new csItems
											 {
												 MOVITEMS_CODEMP = itm.MOVITEMS_CODEMP,
												 MOVITEMS_CODMOV = itm.MOVITEMS_CODMOV,
												 MOVITEMS_PLANTA = itm.MOVITEMS_PLANTA,
												 MOVITEMS_NROMOV = itm.MOVITEMS_NROMOV,
												 MOVITEMS_NROITM = itm.MOVITEMS_NROITM,
												 MOVITEMS_TIPPRO = itm.MOVITEMS_TIPPRO,
												 MOVITEMS_ARTCOD = itm.MOVITEMS_ARTCOD,
												 MOVITEMS_NSERIE = (int)itm.MOVITEMS_NSERIE,
												 MOVITEMS_ENVASE = itm.MOVITEMS_ENVASE,
												 MOVITEMS_NOTROS = itm.MOVITEMS_NOTROS,
												 MOVITEMS_NETO = (decimal)itm.MOVITEMS_NETO,
												 MOVITEMS_BRUTO = (decimal)itm.MOVITEMS_BRUTO,
												 MOVITEMS_TARA = (decimal)itm.MOVITEMS_TARA,
												 MOVITEMS_DESCRP = itm.MOVITEMS_DESCRP

											 }).ToList();
							}


						}catch(Exception) { }
                    }

				}


			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al obtener romaneos de tejeduría", csEstado.eRelevancia.Grave);
			}
			return movimientos;
		}

		public static void CompletarRomaneo(csMovimientos mov)
		{
			Estado.Iniciar();
			try
			{
				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					//using (System.Transactions.TransactionScope tr = new System.Transactions.TransactionScope())
					//{
					var movDB = db.TBL_MOVCAB.Single(x => x.MOVCAB_CODEMP == mov.MOVCAB_CODEMP &&
															x.MOVCAB_CODMOV == mov.MOVCAB_CODMOV &&
															x.MOVCAB_NROMOV == mov.MOVCAB_NROMOV);

					movDB.MOVCAB_ESTADO = csEstados.EstadosMovimiento.TERMINADO;

					var movsItems = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == movDB.MOVCAB_CODEMP &&
															x.MOVITEMS_CODMOV == movDB.MOVCAB_CODMOV &&
															x.MOVITEMS_NROMOV == movDB.MOVCAB_NROMOV);

					movDB.MOVCAB_NETO = movsItems.Sum(y => y.MOVITEMS_NETO);
					movDB.MOVCAB_CANTBULBAL = movsItems.Count();
						
					//movDB.MOVCAB_CANTBULBAL = 	

						//	db.SP_TEJE_PROCESAR(mov.MOVCAB_CODEMP, mov.MOVCAB_CODMOV, mov.MOVCAB_NROMOV);

						//db.SP_TEJE_INS_PALLET(movDB.MOVCAB_CODEMP, movDB.MOVCAB_CODMOV, movDB.MOVCAB_NROMOV);

					db.SaveChanges();
					//	tr.Complete();
					//}
				}

			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al completar romaneo de tejeduría", csEstado.eRelevancia.Grave);
			}
		}

		public static void EliminarRomaneo(csMovimientos mov)
		{
			Estado.Iniciar();
			try
			{
				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					using(System.Transactions.TransactionScope tr = new System.Transactions.TransactionScope())
					{
						var movDB = db.TBL_MOVCAB.Single(x => x.MOVCAB_CODEMP == mov.MOVCAB_CODEMP && x.MOVCAB_CODMOV == mov.MOVCAB_CODMOV && x.MOVCAB_NROMOV == mov.MOVCAB_NROMOV);

						movDB.MOVCAB_ESTADO = csEstados.EstadosMovimiento.BORRADO;

						var items = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == movDB.MOVCAB_CODEMP && x.MOVITEMS_CODMOV == movDB.MOVCAB_CODMOV && x.MOVITEMS_NROMOV == movDB.MOVCAB_NROMOV);

						foreach(var item in items)
						{
							db.TBL_STRMVK.Single(y => y.STRMVK_TIPPRO == item.MOVITEMS_TIPPRO   &&
														y.STRMVK_ARTCOD == item.MOVITEMS_ARTCOD &&
														y.STRMVK_NSERIE == item.MOVITEMS_NSERIE)
														.STRMVK_ESTADO = csEstados.EstadosMovimiento.TERMINADO;
							
						}
							
						db.SaveChanges();
						tr.Complete();
					}
				}

			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al eliminar romaneo", csEstado.eRelevancia.Grave);
			}
		}

		public static void CancelarArmadoPalletsTejeduria(csMovimientos movArmadoPallet)
		{
			Estado.Iniciar();
			try
			{
				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					using (System.Transactions.TransactionScope tr = new System.Transactions.TransactionScope()) { 

						db.TBL_MOVCAB.Single(x => x.MOVCAB_CODEMP == movArmadoPallet.MOVCAB_CODEMP &&
												x.MOVCAB_CODMOV == movArmadoPallet.MOVCAB_CODMOV &&
												x.MOVCAB_NROMOV == movArmadoPallet.MOVCAB_NROMOV).MOVCAB_ESTADO = csEstados.EstadosMovimiento.BORRADO;
						
						var items = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == movArmadoPallet.MOVCAB_CODEMP && x.MOVITEMS_CODMOV == movArmadoPallet.MOVCAB_CODMOV && x.MOVITEMS_NROMOV == movArmadoPallet.MOVCAB_NROMOV && x.MOVITEMS_PRINCIPAL != true);

						foreach(var item in items)
						{
							db.TBL_STRMVK.Single(x => x.STRMVK_TIPPRO == item.MOVITEMS_TIPPRO &&
													  x.STRMVK_ARTCOD == item.MOVITEMS_ARTCOD &&
													  x.STRMVK_NSERIE == item.MOVITEMS_NSERIE)
													  .STRMVK_ESTADO = csEstados.EstadosMovimiento.TERMINADO;
						}

					
						db.SaveChanges();
						tr.Complete();
					}
				}

			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Ocurrió un error al cancelar armado de pallets", csEstado.eRelevancia.Grave);
			}
		}

		public static void EliminarItemRomaneo(csItems item)
		{
			Estado.Iniciar();
			try
			{
				using (var db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion)) {

					using (var tran = new System.Transactions.TransactionScope()) {

						var movDB = db.TBL_MOVCAB.Single(x =>   x.MOVCAB_CODEMP == item.MOVITEMS_CODEMP &&
																x.MOVCAB_CODMOV == item.MOVITEMS_CODMOV &&
																x.MOVCAB_NROMOV == item.MOVITEMS_NROMOV);

						if (item == null) {
							throw new NullReferenceException("El romaneo no existe");
						}

						if (movDB.MOVCAB_ESTADO != Geshil.BI.csEstados.EstadosMovimiento.HABILITADO)
						{
							throw new Exception("El romaneo ya no se encuentra habilitado");
						}

						var movitm = db.TBL_MOVITEMS.Single(x => x.MOVITEMS_CODEMP == item.MOVITEMS_CODEMP && x.MOVITEMS_CODMOV == item.MOVITEMS_CODMOV && x.MOVITEMS_NSERIE == item.MOVITEMS_NSERIE);

						var stk = db.TBL_STRMVK.Single(x => x.STRMVK_TIPPRO == movitm.MOVITEMS_TIPPRO && x.STRMVK_NSERIE == movitm.MOVITEMS_NSERIE && x.STRMVK_ARTCOD == movitm.MOVITEMS_ARTCOD);

						db.Entry(movitm).State = System.Data.Entity.EntityState.Deleted;

						stk.STRMVK_ESTADO = Geshil.BI.csEstados.EstadosMovimiento.TERMINADO;

						db.SaveChanges();

						tran.Complete();

					}
				}
			}
			catch (Exception ex)
			{
				Estado.CapturarError(ex, "Error al eliminar item de romaneo de tejeduría", csEstado.eRelevancia.Grave);
			}
		}

		public static csItems GrabarItemRecepcion(csMovimientos movimiento, csItems item)
		{
			Estado.Iniciar();
			csItems itm = new csItems();
			try
			{
				using (DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{

					//var itmStock = db.TBL_STRMVK.Where(x => x.STRMVK_TIPPRO == item.MOVITEMS_TIPPRO && x.STRMVK_ARTCOD == item.MOVITEMS_ARTCOD &&
					//									x.STRMVK_NSERIE == item.MOVITEMS_NSERIE).FirstOrDefault();

					var itmStock = new DAL.TBL_STRMVK
					{
						STRMVK_TIPPRO = item.MOVITEMS_TIPPRO,
						STRMVK_ARTCOD = item.MOVITEMS_ARTCOD,
						STRMVK_NSERIE = item.MOVITEMS_NSERIE,
						STRMVK_NETO = item.MOVITEMS_NETO,
						STRMVK_BRUTO = item.MOVITEMS_BRUTO,
						STRMVK_TARA = item.MOVITEMS_TARA,
						STRMVK_NOTROS = item.MOVITEMS_NOTROS,
						STRMVK_PLANTA = Seguridad.BI.csPlantas.MiPlanta.PC_PLANTA_COD,
						STRMVK_FALLA_ID = item.MOVITEMS_FALLA_ID,
						STRMVK_NATRIB = csPartida.SIN_PARTIDA,
						STRMVK_DEPOSI = csDeposito.TipoDepositos.CRUDO,
						STRMVK_SECTOR = csSector.TipoSectores.TEJEDURIA,
						STRMVK_ESTADO = csEstados.EstadosMovimiento.TERMINADO,
						STRMVK_ENVASE = csEnvase.CRUDO,
						STRMVK_FECALT = DateTime.Now
					};

					if (itmStock == null)
					{
						throw new Exceptions.InvalidDataException("La pieza no existe");
					}

					//if (db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == movimiento.MOVCAB_CODEMP && x.MOVITEMS_CODMOV == movimiento.MOVCAB_CODMOV && x.MOVITEMS_NROMOV == movimiento.MOVCAB_NROMOV && x.MOVITEMS_NSERIE == item.MOVITEMS_NSERIE).Count() > 0)
					//{
					//	throw new Exceptions.InvalidDataException("La pieza ya fué ingresada");
					//}

					if (itmStock.STRMVK_ESTADO != csEstados.EstadosMovimiento.TERMINADO)
					{
						throw new Exceptions.InvalidDataException("La pieza no está disponible (no se encuentra en un estado correcto)");
					}

					if (itmStock.STRMVK_NOTROS != movimiento.MOVCAB_NOTROS)
					{
						throw new Exceptions.InvalidDataException("La calidad de la pieza no corresponde con la calidad del pallet");
					}

					//var movsItemArmado = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == movimiento.MOVCAB_CODEMP &&
					//							 x.MOVITEMS_CODMOV == csEstados.TipoMovimiento.RECTJ &&
					//							 x.MOVITEMS_TIPPRO == itmStock.STRMVK_TIPPRO &&
					//							 x.MOVITEMS_ARTCOD == itmStock.STRMVK_ARTCOD &&
					//							 x.MOVITEMS_NSERIE == itmStock.STRMVK_NSERIE &&
					//							 x.MOVITEMS_NROMOV != movimiento.MOVCAB_NROMOV
					//							 );

					//if (movsItemArmado.Count() > 0)
					//{
					//	if (db.TBL_MOVCAB.Where(x => x.MOVCAB_CODEMP == movimiento.MOVCAB_CODEMP &&
					//								x.MOVCAB_CODMOV == csEstados.TipoMovimiento.RECTJ &&
					//								movsItemArmado.Where(y => y.MOVITEMS_NROMOV == x.MOVCAB_NROMOV).Count() > 0 &&
					//								x.MOVCAB_ESTADO != csEstados.EstadosMovimiento.BORRADO).Count() > 0)
					//	{
					//		throw new Exceptions.InvalidDataException("La pieza ya fué armada en otro pallet.");
					//	}
					//}


					DAL.TBL_MOVITEMS movitem = new DAL.TBL_MOVITEMS();

					movitem.MOVITEMS_CODEMP = movimiento.MOVCAB_CODEMP;
					movitem.MOVITEMS_PLANTA = movimiento.MOVCAB_PLANTA;
					movitem.MOVITEMS_CODMOV = movimiento.MOVCAB_CODMOV;
					movitem.MOVITEMS_NROMOV = movimiento.MOVCAB_NROMOV;

					try
					{

						movitem.MOVITEMS_NROITM = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == movimiento.MOVCAB_CODEMP &&
																			 x.MOVITEMS_CODMOV == movimiento.MOVCAB_CODMOV &&
																			 x.MOVITEMS_NROMOV == movimiento.MOVCAB_NROMOV)
																			 .Max(y => y.MOVITEMS_NROITM) + 1;

					}
					catch (Exception)
					{
						movitem.MOVITEMS_NROITM = 1;
					}

					movitem.MOVITEMS_DEPOSI = csDeposito.TipoDepositos.CRUDO;
					movitem.MOVITEMS_SECTOR = csSector.TipoSectores.TEJEDURIA;
					movitem.MOVITEMS_TIPPRO = itmStock.STRMVK_TIPPRO;
					movitem.MOVITEMS_ARTCOD = itmStock.STRMVK_ARTCOD;
					movitem.MOVITEMS_NSERIE = itmStock.STRMVK_NSERIE;
					movitem.MOVITEMS_ENVASE = csEnvase.CRUDO;
					movitem.MOVITEMS_NATRIB = csPartida.SIN_PARTIDA;
					movitem.MOVITEMS_NOTROS = (string.IsNullOrEmpty(itmStock.STRMVK_NOTROS) ? string.Empty : itmStock.STRMVK_NOTROS);
					movitem.MOVITEMS_DESCRP = item.MOVITEMS_DESCRP;
					movitem.MOVITEMS_UNIMED = "K";
					movitem.MOVITEMS_PESAJE_MANUAL = false;
					movitem.MOVITEMS_TRANSFERIDO = false;
					movitem.MOVITEMS_VUELTASXMINUTO = 0;
					movitem.MOVITEMS_VUELTASXROLLO = 0;
					movitem.MOVITEMS_OBSERV = string.Empty;
					movitem.MOVITEMS_NETO = itmStock.STRMVK_NETO;
					movitem.MOVITEMS_TARA = itmStock.STRMVK_TARA;
					movitem.MOVITEMS_BRUTO = itmStock.STRMVK_BRUTO;
					movitem.MOVITEMS_PRINCIPAL = false;
					movitem.MOVITEMS_ES_AUXILIAR = false;
					movitem.MOVITEMS_FALLA_ID = int.Parse(item.MOVITEMS_FALLA_ID);

					db.TBL_MOVITEMS.Add(movitem);
					db.TBL_STRMVK.Add(itmStock);

					db.SaveChanges();

					itm.MOVITEMS_CODEMP = movitem.MOVITEMS_CODEMP;
					itm.MOVITEMS_CODMOV = movitem.MOVITEMS_CODMOV;
					itm.MOVITEMS_PLANTA = movitem.MOVITEMS_PLANTA;
					itm.MOVITEMS_NROMOV = movitem.MOVITEMS_NROMOV;
					itm.MOVITEMS_NROITM = movitem.MOVITEMS_NROITM;
					itm.MOVITEMS_ARTCOD = movitem.MOVITEMS_ARTCOD;
					itm.MOVITEMS_TIPPRO = movitem.MOVITEMS_TIPPRO;
					itm.MOVITEMS_NSERIE = (int)movitem.MOVITEMS_NSERIE;
					itm.MOVITEMS_DESCRP = movitem.MOVITEMS_DESCRP;
					itm.MOVITEMS_NETO = (decimal)movitem.MOVITEMS_NETO;
					itm.MOVITEMS_BRUTO = (decimal)movitem.MOVITEMS_BRUTO;
					itm.MOVITEMS_TARA = (decimal)movitem.MOVITEMS_TARA;
					itm.MOVITEMS_NOTROS = movitem.MOVITEMS_NOTROS;
					itm.MOVITEMS_FALLA_ID = movitem.MOVITEMS_FALLA_ID.ToString();
				}

			}
			catch (Exceptions.InvalidDataException ex)
			{
				Estado.CapturarError(ex, "Ocurrió un error de validación", csEstado.eRelevancia.Normal);
			}
			catch (Exception ex)
			{
				Estado.CapturarError(ex, "Error al grabar item de armado de pallet", csEstado.eRelevancia.Grave);
			}

			return itm;
		}

		public static csItems GrabarItemArmadoPallet(csMovimientos movArmadoPallet, csItems item)
		{
			Estado.Iniciar();
			csItems itm = new csItems();
            try
			{
				using (DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{

					var itmStock = db.TBL_STRMVK.Where(x => x.STRMVK_TIPPRO == item.MOVITEMS_TIPPRO && x.STRMVK_ARTCOD == item.MOVITEMS_ARTCOD &&
														x.STRMVK_NSERIE == item.MOVITEMS_NSERIE).FirstOrDefault();

					if (itmStock == null)
					{
						throw new Exceptions.InvalidDataException("La pieza no existe");
					}

					if (db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == movArmadoPallet.MOVCAB_CODEMP && x.MOVITEMS_CODMOV == movArmadoPallet.MOVCAB_CODMOV && x.MOVITEMS_NROMOV == movArmadoPallet.MOVCAB_NROMOV && x.MOVITEMS_NSERIE == item.MOVITEMS_NSERIE).Count() > 0)
					{
						throw new Exceptions.InvalidDataException("La pieza ya fué ingresada");
					}

					if (itmStock.STRMVK_ESTADO != csEstados.EstadosMovimiento.TERMINADO)
					{
						throw new Exceptions.InvalidDataException("La pieza no está disponible (no se encuentra en un estado correcto)");
					}

					if (itmStock.STRMVK_NOTROS != movArmadoPallet.MOVCAB_NOTROS)
					{
						throw new Exceptions.InvalidDataException("La calidad de la pieza no corresponde con la calidad del pallet");
					}

					var movsItemArmado = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == movArmadoPallet.MOVCAB_CODEMP &&
												 x.MOVITEMS_CODMOV == csEstados.TipoMovimiento.ARPLTJ &&
												 x.MOVITEMS_TIPPRO == itmStock.STRMVK_TIPPRO &&
												 x.MOVITEMS_ARTCOD == itmStock.STRMVK_ARTCOD &&
												 x.MOVITEMS_NSERIE == itmStock.STRMVK_NSERIE &&
												 x.MOVITEMS_NROMOV != movArmadoPallet.MOVCAB_NROMOV
												 );

					if(movsItemArmado.Count() > 0)
					{
						if(db.TBL_MOVCAB.Where(x=> x.MOVCAB_CODEMP == movArmadoPallet.MOVCAB_CODEMP &&
												   x.MOVCAB_CODMOV == csEstados.TipoMovimiento.ARPLTJ &&
												   movsItemArmado.Where(y=>y.MOVITEMS_NROMOV == x.MOVCAB_NROMOV).Count() > 0 &&
												   x.MOVCAB_ESTADO != csEstados.EstadosMovimiento.BORRADO ).Count() > 0)
						{
							throw new Exceptions.InvalidDataException("La pieza ya fué armada en otro pallet.");
						}
					}
					

					DAL.TBL_MOVITEMS movitem = new DAL.TBL_MOVITEMS();
					movitem.MOVITEMS_CODEMP = movArmadoPallet.MOVCAB_CODEMP;
					movitem.MOVITEMS_PLANTA = movArmadoPallet.MOVCAB_PLANTA;
					movitem.MOVITEMS_CODMOV = movArmadoPallet.MOVCAB_CODMOV;
					movitem.MOVITEMS_NROMOV = movArmadoPallet.MOVCAB_NROMOV;

					try {

						movitem.MOVITEMS_NROITM = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == movArmadoPallet.MOVCAB_CODEMP &&
																			 x.MOVITEMS_CODMOV == movArmadoPallet.MOVCAB_CODMOV &&
																			 x.MOVITEMS_NROMOV == movArmadoPallet.MOVCAB_NROMOV)
																			 .Max(y => y.MOVITEMS_NROITM) + 1;

					} catch (Exception)
					{
						movitem.MOVITEMS_NROITM = 1;
					}

					movitem.MOVITEMS_DEPOSI = "TERMINA";
					movitem.MOVITEMS_SECTOR = "S/A";
					movitem.MOVITEMS_TIPPRO = itmStock.STRMVK_TIPPRO;
					movitem.MOVITEMS_ARTCOD = itmStock.STRMVK_ARTCOD;
					movitem.MOVITEMS_NSERIE = itmStock.STRMVK_NSERIE;
					movitem.MOVITEMS_ENVASE = (string.IsNullOrEmpty(itmStock.STRMVK_ENVASE) ? string.Empty : itmStock.STRMVK_ENVASE);
					movitem.MOVITEMS_NOTROS = (string.IsNullOrEmpty(itmStock.STRMVK_NOTROS) ? string.Empty : itmStock.STRMVK_NOTROS);
					movitem.MOVITEMS_DESCRP = item.MOVITEMS_DESCRP;
					movitem.MOVITEMS_UNIMED = "K";
					movitem.MOVITEMS_PESAJE_MANUAL = false;
					movitem.MOVITEMS_TRANSFERIDO = false;
					movitem.MOVITEMS_VUELTASXMINUTO = 0;
					movitem.MOVITEMS_VUELTASXROLLO = 0;
					movitem.MOVITEMS_OBSERV = string.Empty;
					movitem.MOVITEMS_NETO = itmStock.STRMVK_NETO;
					movitem.MOVITEMS_TARA = itmStock.STRMVK_TARA;
					movitem.MOVITEMS_BRUTO = itmStock.STRMVK_BRUTO;
					movitem.MOVITEMS_PRINCIPAL = false;
					movitem.MOVITEMS_ES_AUXILIAR = false;
					db.TBL_MOVITEMS.Add(movitem);

					db.SaveChanges();

					itm.MOVITEMS_CODEMP = movitem.MOVITEMS_CODEMP;
					itm.MOVITEMS_CODMOV = movitem.MOVITEMS_CODMOV;
					itm.MOVITEMS_PLANTA = movitem.MOVITEMS_PLANTA;
					itm.MOVITEMS_NROMOV = movitem.MOVITEMS_NROMOV;
					itm.MOVITEMS_NROITM = movitem.MOVITEMS_NROITM;
					itm.MOVITEMS_ARTCOD = movitem.MOVITEMS_ARTCOD;
					itm.MOVITEMS_TIPPRO = movitem.MOVITEMS_TIPPRO;
					itm.MOVITEMS_NSERIE = (int)movitem.MOVITEMS_NSERIE;
					itm.MOVITEMS_DESCRP = movitem.MOVITEMS_DESCRP;
					itm.MOVITEMS_NETO = (decimal)movitem.MOVITEMS_NETO;
					itm.MOVITEMS_BRUTO = (decimal)movitem.MOVITEMS_BRUTO;
					itm.MOVITEMS_TARA = (decimal)movitem.MOVITEMS_TARA;
					itm.MOVITEMS_NOTROS = movitem.MOVITEMS_NOTROS;

                }

			}catch(Exceptions.InvalidDataException ex)
			{
				Estado.CapturarError(ex, "Ocurrió un error de validación" , csEstado.eRelevancia.Normal);
			}
			catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al grabar item de armado de pallet", csEstado.eRelevancia.Grave);
			}

			return itm;
        }

		public static void EliminarPiezaArmadoPallet(csMovimientos movArmadoPallet, csItems item)
		{
			Estado.Iniciar();
			try
			{
				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					using(System.Transactions.TransactionScope tr = new System.Transactions.TransactionScope())
					{
						var itm = db.TBL_MOVITEMS.Single(x => x.MOVITEMS_CODEMP == item.MOVITEMS_CODEMP && x.MOVITEMS_CODMOV == item.MOVITEMS_CODMOV && x.MOVITEMS_NROMOV == item.MOVITEMS_NROMOV && x.MOVITEMS_NROITM == item.MOVITEMS_NROITM);

						db.TBL_MOVITEMS.Remove(itm);

						var pieza = db.TBL_STRMVK.Single(x => x.STRMVK_TIPPRO == item.MOVITEMS_TIPPRO && x.STRMVK_ARTCOD == item.MOVITEMS_ARTCOD && x.STRMVK_NSERIE == item.MOVITEMS_NSERIE);
						pieza.STRMVK_ESTADO = csEstados.EstadosMovimiento.TERMINADO;

						db.SaveChanges();
						tr.Complete();
					}
				}
			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al eliminar item de armado de pallet tejeduría");
			}
		}

		public static void CompletarArmadoPallets(csMovimientos movArmadoPallet)
		{
			Estado.Iniciar();
			try
			{
				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					using (System.Transactions.TransactionScope tr = new System.Transactions.TransactionScope()) {

						var mov = db.TBL_MOVCAB.Single(x => x.MOVCAB_CODEMP == movArmadoPallet.MOVCAB_CODEMP &&
													x.MOVCAB_CODMOV == movArmadoPallet.MOVCAB_CODMOV &
													x.MOVCAB_NROMOV == movArmadoPallet.MOVCAB_NROMOV);
													
						var movsItems = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == movArmadoPallet.MOVCAB_CODEMP &&
																x.MOVITEMS_CODMOV == movArmadoPallet.MOVCAB_CODMOV &&
																x.MOVITEMS_NROMOV == movArmadoPallet.MOVCAB_NROMOV);

						mov.MOVCAB_NETO = movsItems.Sum(y => y.MOVITEMS_NETO);
						mov.MOVCAB_BRUTO = movsItems.Sum(y => y.MOVITEMS_BRUTO);
						mov.MOVCAB_TARA = movsItems.Sum(y => y.MOVITEMS_TARA);
						mov.MOVCAB_CANTBULBAL = movsItems.Count();
						mov.MOVCAB_CANTBULCPTEORI = mov.MOVCAB_CANTBULBAL;

						mov.MOVCAB_ESTADO = csEstados.EstadosMovimiento.TERMINADO;

						db.SaveChanges();

						tr.Complete();
					}
				}
			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al completar armado de pallets", csEstado.eRelevancia.Grave);
			}
		}

		public static void CompletarArmadoPallets()
		{
			throw new NotImplementedException();
		}

		public static List<csItems> GetAllPlanificaciones(string empresa)
		{
			Estado.Iniciar();

			List<csItems> items = new List<csItems>();

			try {
				using (DAL.GeshilEntities ent = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					items = (from a in ent.FN_GET_VTEJES(Seguridad.BI.csUsuario.Empresa)
							 select new csItems
							 {
								 MOVITEMS_CODEMP = a.MOVITEMS_CODEMP,
								 MOVITEMS_CODMOV = a.MOVITEMS_CODMOV,
								 MOVITEMS_NROMOV = a.MOVITEMS_NROMOV,
								 MOVITEMS_TIPPRO = a.MOVITEMS_TIPPRO,
								 MOVITEMS_ARTCOD = a.MOVITEMS_ARTCOD,
								 MOVITEMS_VUELTASXMINUTO = (decimal)a.MOVITEMS_VUELTASXMINUTO,
								 MOVITEMS_VUELTASXROLLO = (decimal)a.MOVITEMS_VUELTASXROLLO,
								 MOVITEMS_ESTADO = a.MOVCAB_ESTADO,
								 MOVITEMS_DESCRP = a.MOVITEMS_DESCRP,
								 MOVITEMS_FECALT = a.MOVCAB_FECALT,
								 MOVITEMS_PROG_NRO = (int)a.MOVCAB_PROG_NRO,
								 MOVITEMS_TEJE_MAQ = (int)a.MOVCAB_TEJE_MAQ,

							 }).ToList();
				}

			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al obtener planificaciones de tejeduría ");
			}

			return items;
		}

		public static csMovimientos GrabarArmadoPallet(csItems producto)
		{
			Estado.Iniciar();
			csMovimientos mov = new csMovimientos();
			try { 
				using(Geshil.DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					using(System.Transactions.TransactionScope tr = new System.Transactions.TransactionScope()) { 

						DAL.TBL_MOVCAB movcab = new DAL.TBL_MOVCAB();
						movcab.MOVCAB_CODEMP = Seguridad.BI.csUsuario.Empresa;
						movcab.MOVCAB_CODMOV = csEstados.TipoMovimiento.ARPLTJ;
						movcab.MOVCAB_PLANTA = Seguridad.BI.csPlantas.MiPlanta.PC_PLANTA_COD;
						movcab.MOVCAB_NOTROS = producto.MOVITEMS_NOTROS;

						try
						{
							var movs = db.TBL_MOVCAB.Where(x => x.MOVCAB_CODEMP == movcab.MOVCAB_CODEMP && x.MOVCAB_CODMOV == movcab.MOVCAB_CODMOV);
							movcab.MOVCAB_NROMOV = (movs.Count() == 0 ? 1 : movs.Max(y => y.MOVCAB_NROMOV) + 1);
						}
						catch (Exception) { movcab.MOVCAB_NROMOV = 1; }

						movcab.MOVCAB_TIPOCPTEORI = "REM";
						movcab.MOVCAB_CANTKGCPTEORI = 0;
						movcab.MOVCAB_CANTBULCPTEORI = 0;
						movcab.MOVCAB_CANTKGBAL = 0;
						movcab.MOVCAB_CANTBULBAL = 0;
						movcab.MOVCAB_FECALT = DateTime.Now;
						movcab.MOVCAB_USERID = Seguridad.BI.csUsuario.Login;
						movcab.MOVCAB_ESTADO = csEstados.EstadosMovimiento.HABILITADO;
						movcab.MOVCAB_OBJETADO = false;
						movcab.MOVCAB_EXCEPCION = string.Empty;
						movcab.MOVCAB_PESAJE_MANUAL = false;
						movcab.MOVCAB_TARA_UNIDAD = 0;
						movcab.MOVCAB_TIPPRO = producto.MOVITEMS_TIPPRO;
						movcab.MOVCAB_ARTCOD = producto.MOVITEMS_ARTCOD;

						db.TBL_MOVCAB.Add(movcab);

						DAL.TBL_MOVITEMS item = new DAL.TBL_MOVITEMS();
						item.MOVITEMS_CODEMP = movcab.MOVCAB_CODEMP;
						item.MOVITEMS_PLANTA = movcab.MOVCAB_PLANTA;
						item.MOVITEMS_CODMOV = movcab.MOVCAB_CODMOV;
						item.MOVITEMS_NROMOV = movcab.MOVCAB_NROMOV;
						item.MOVITEMS_NROITM = 1;
						item.MOVITEMS_DEPOSI = "TERMINA";
						item.MOVITEMS_SECTOR = "S/A";
						item.MOVITEMS_TIPPRO = producto.MOVITEMS_TIPPRO;
						item.MOVITEMS_ARTCOD = producto.MOVITEMS_ARTCOD;
                        item.MOVITEMS_NSERIE = 0;
						item.MOVITEMS_ENVASE = string.Empty;
						item.MOVITEMS_NOTROS = producto.MOVITEMS_NOTROS;
						item.MOVITEMS_DESCRP = producto.MOVITEMS_DESCRP;
                        item.MOVITEMS_UNIMED = "K";
						item.MOVITEMS_PESAJE_MANUAL = false;
						item.MOVITEMS_TRANSFERIDO = false;
						item.MOVITEMS_PRINCIPAL = true;
						item.MOVITEMS_VUELTASXMINUTO = 0;
						item.MOVITEMS_VUELTASXROLLO = 0;
						item.MOVITEMS_NETO = 0;
						item.MOVITEMS_BRUTO = 0;
						item.MOVITEMS_TARA = 0;
						item.MOVITEMS_OBSERV = string.Empty;
						item.MOVITEMS_ES_AUXILIAR = false;
						db.TBL_MOVITEMS.Add(item);

						db.SaveChanges();
						tr.Complete();

						mov.MOVCAB_NOTROS = item.MOVITEMS_NOTROS;
						mov.MOVCAB_CODEMP = movcab.MOVCAB_CODEMP;
						mov.MOVCAB_CODMOV = movcab.MOVCAB_CODMOV;
						mov.MOVCAB_NROMOV = movcab.MOVCAB_NROMOV;
						mov.MOVCAB_ESTADO = movcab.MOVCAB_ESTADO;
						mov.MOVCAB_FECALT = movcab.MOVCAB_FECALT;
						mov.MOVCAB_ARTCOD = movcab.MOVCAB_ARTCOD;
						mov.MOVCAB_TIPPRO = movcab.MOVCAB_TIPPRO;
						mov.MOVCAB_PLANTA = movcab.MOVCAB_PLANTA;
						mov.MOVCAB_ARTCOD = item.MOVITEMS_ARTCOD;
						mov.Items = new List<csItems>();
                    }
				}

			}
			catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al grabar armado de pallet tejeduría");
			}

			return mov;
        }

		public static List<csItems> GetDetallePesaje(csMovimientos movPesaje)
		{
			Estado.Iniciar();
			List<csItems> listaDetallePesaje = new List<csItems>();

            try { 

				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					var itemsDB =(from a in db.TBL_MOVITEMS
							where
								a.MOVITEMS_CODEMP == movPesaje.MOVCAB_CODEMP &&
								a.MOVITEMS_CODMOV == movPesaje.MOVCAB_CODMOV &&
								a.MOVITEMS_NROMOV == movPesaje.MOVCAB_NROMOV
							select a).ToList();

					foreach(var a in itemsDB)
					{
						var item = new csItems
						{
							MOVITEMS_CODEMP = a.MOVITEMS_CODEMP,
							MOVITEMS_CODMOV = a.MOVITEMS_CODMOV,
							MOVITEMS_NROMOV = a.MOVITEMS_NROMOV,
							MOVITEMS_PLANTA = a.MOVITEMS_PLANTA,
							MOVITEMS_TIPPRO = a.MOVITEMS_TIPPRO,
							MOVITEMS_ARTCOD = a.MOVITEMS_ARTCOD,
							MOVITEMS_NSERIE = (int)a.MOVITEMS_NSERIE,
							MOVITEMS_NROITM = a.MOVITEMS_NROITM,
							MOVITEMS_CANTID = a.MOVITEMS_CANTID,
							MOVITEMS_NATRIB = a.MOVITEMS_NATRIB,
							MOVITEMS_DESCRP = a.MOVITEMS_DESCRP,
							MOVITEMS_DEBAJA = a.MOVITEMS_DEBAJA,
							MOVITEMS_BRUTO = (decimal)a.MOVITEMS_BRUTO,
							MOVITEMS_NETO = (decimal)a.MOVITEMS_NETO,
							MOVITEMS_TARA = (decimal)a.MOVITEMS_TARA,
							MOVITEMS_OBSERV = a.MOVITEMS_OBSERV,
							MOVITEMS_NOTROS = a.MOVITEMS_NOTROS,
							MOVITEMS_CNTSEC = a.MOVITEMS_CNTSEC
						};

                        item.MOVITEMS_FALLA_ID = a.MOVITEMS_FALLA_ID.ToString();

						listaDetallePesaje.Add(item);
                    }
				}

			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al obtener detalle de pesaje");
			}

			return listaDetallePesaje;
        }

		public static decimal GetNroPrograma()
		{
			int nroPrograma = 0;

			using (DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
			{
				try {
					nroPrograma = (int)db.TBL_MOVCAB.Where(x => x.MOVCAB_CODEMP == Seguridad.BI.csUsuario.Empresa &&
													   x.MOVCAB_CODMOV == csEstados.TipoMovimiento.PLANTJ).Max(y => y.MOVCAB_PROG_NRO);
                }
				catch (Exception)
				{

				}
            }

			return nroPrograma + 1;
		}

		public static List<csItems> GetDetalle(csItems Principal)
		{
			List<csItems> items = new List<csItems>();

			using (DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
			{
				 items = (from a in db.FN_GET_VTEJES_DET(Principal.MOVITEMS_CODEMP, Principal.MOVITEMS_NROMOV)
						 select new csItems
						 {
							 MOVITEMS_CODEMP = a.MOVITEMS_CODEMP,
							 MOVITEMS_CODMOV = a.MOVITEMS_CODMOV,
							 MOVITEMS_NROMOV = a.MOVITEMS_NROMOV,
							 MOVITEMS_ARTCOD = a.MOVITEMS_ARTCOD,
							 MOVITEMS_TIPPRO = a.MOVITEMS_TIPPRO,
							 MOVITEMS_PORCENTAJE = (decimal)(a.MOVITEMS_PORCENTAJE == null ? 0:a.MOVITEMS_PORCENTAJE),
							 MOVITEMS_BRUTO = (decimal)(a.MOVITEMS_BRUTO == null ? 0 : a.MOVITEMS_BRUTO),
							 MOVITEMS_DESCRP = a.MOVITEMS_DESCRP,
							 MOVITEMS_NETO = (decimal)(a.MOVITEMS_NETO == null ? 0 : a.MOVITEMS_NETO) ,
							 MOVITEMS_TARA = (decimal)(a.MOVITEMS_TARA == null ? 0 : a.MOVITEMS_TARA),
							 MOVITEMS_ENVASE = a.MOVITEMS_ENVASE,
							 MOVITEMS_PRINCIPAL = (bool)(a.MOVITEMS_PRINCIPAL == null ? false : a.MOVITEMS_PRINCIPAL),
							 MOVITEMS_OBSERV = a.MOVITEMS_OBSERV
							 
                         }).ToList();
			}

			return items;
		}

		public static void GrabarOrden(csItems productoVtje, List<csItems> Composicion)
		{
			Estado.Iniciar();

			try { 

				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					using (System.Transactions.TransactionScope tr = new System.Transactions.TransactionScope())
					{
						DAL.TBL_MOVCAB movcab = new DAL.TBL_MOVCAB();
						movcab.MOVCAB_CODEMP = Seguridad.BI.csUsuario.Empresa;
						movcab.MOVCAB_CODMOV = csEstados.TipoMovimiento.PLANTJ;
						movcab.MOVCAB_PLANTA = Seguridad.BI.csPlantas.MiPlanta.PC_PLANTA_COD;

						try {
							var movs = db.TBL_MOVCAB.Where(x => x.MOVCAB_CODEMP == movcab.MOVCAB_CODEMP && x.MOVCAB_CODMOV == movcab.MOVCAB_CODMOV);
							movcab.MOVCAB_NROMOV = (movs.Count() == 0 ? 1 : movs.Max(y => y.MOVCAB_NROMOV) + 1);
						}
						catch (Exception) { movcab.MOVCAB_NROMOV = 1; }

						movcab.MOVCAB_TIPOCPTEORI = "REM";
						movcab.MOVCAB_CANTKGCPTEORI = 0;
						movcab.MOVCAB_CANTBULCPTEORI = 0;
						movcab.MOVCAB_CANTKGBAL = 0;
						movcab.MOVCAB_CANTBULBAL = 0;
						movcab.MOVCAB_FECALT = DateTime.Now;
						movcab.MOVCAB_USERID = Seguridad.BI.csUsuario.Login;
						movcab.MOVCAB_ESTADO = csEstados.EstadosMovimiento.TERMINADO;
						movcab.MOVCAB_OBJETADO = false;
						movcab.MOVCAB_EXCEPCION = string.Empty;
						movcab.MOVCAB_PESAJE_MANUAL = false;
						movcab.MOVCAB_TARA_UNIDAD = 0;
						movcab.MOVCAB_TEJE_MAQ = productoVtje.MOVITEMS_TEJE_MAQ;
						movcab.MOVCAB_PROG_NRO = productoVtje.MOVITEMS_PROG_NRO;

						db.TBL_MOVCAB.Add(movcab);
						db.SaveChanges();

						DAL.TBL_MOVITEMS item = new DAL.TBL_MOVITEMS();
						item.MOVITEMS_CODEMP = movcab.MOVCAB_CODEMP;
						item.MOVITEMS_PLANTA = movcab.MOVCAB_PLANTA;
						item.MOVITEMS_CODMOV = movcab.MOVCAB_CODMOV;
						item.MOVITEMS_NROMOV = movcab.MOVCAB_NROMOV;
						item.MOVITEMS_NROITM = 1;
						item.MOVITEMS_DEPOSI = "TERMINA";
						item.MOVITEMS_SECTOR = "S/A";
						item.MOVITEMS_TIPPRO = productoVtje.MOVITEMS_TIPPRO;
						item.MOVITEMS_ARTCOD = productoVtje.MOVITEMS_ARTCOD;
						item.MOVITEMS_NSERIE = 0;
						item.MOVITEMS_ENVASE = string.Empty;
						item.MOVITEMS_NOTROS = string.Empty;
						item.MOVITEMS_DESCRP = productoVtje.MOVITEMS_DESCRP;
						item.MOVITEMS_UNIMED = "K";
						item.MOVITEMS_PESAJE_MANUAL = false;
						item.MOVITEMS_TRANSFERIDO = false;
						item.MOVITEMS_PRINCIPAL = true;
						item.MOVITEMS_VUELTASXMINUTO = productoVtje.MOVITEMS_VUELTASXMINUTO;
						item.MOVITEMS_VUELTASXROLLO = productoVtje.MOVITEMS_VUELTASXROLLO;
						item.MOVITEMS_OBSERV = "Principal";

						db.TBL_MOVITEMS.Add(item);

						int nro = 1;
						foreach(var comp in Composicion)
						{
							item = new DAL.TBL_MOVITEMS();
							item.MOVITEMS_CODEMP = movcab.MOVCAB_CODEMP;
							item.MOVITEMS_PLANTA = movcab.MOVCAB_PLANTA;
							item.MOVITEMS_CODMOV = movcab.MOVCAB_CODMOV;
							item.MOVITEMS_NROMOV = movcab.MOVCAB_NROMOV;
							item.MOVITEMS_NROITM = ++nro;
							item.MOVITEMS_DEPOSI = "TERMINA";
							item.MOVITEMS_SECTOR = "S/A";
							item.MOVITEMS_TIPPRO = comp.MOVITEMS_TIPPRO;
							item.MOVITEMS_ARTCOD = comp.MOVITEMS_ARTCOD;
							item.MOVITEMS_NSERIE = 0;
							item.MOVITEMS_ENVASE = string.Empty;
							item.MOVITEMS_NOTROS = string.Empty;
							item.MOVITEMS_DESCRP = comp.MOVITEMS_DESCRP;
							item.MOVITEMS_UNIMED = "K";
							item.MOVITEMS_PESAJE_MANUAL = false;
							item.MOVITEMS_TRANSFERIDO = false;
							item.MOVITEMS_PRINCIPAL = false;
							item.MOVITEMS_ES_AUXILIAR = true;
							item.MOVITEMS_VUELTASXMINUTO = 0;
							item.MOVITEMS_VUELTASXROLLO = 0;
							item.MOVITEMS_OBSERV = "Composición";
							item.MOVITEMS_PORCENTAJE = comp.MOVITEMS_PORCENTAJE;

							db.TBL_MOVITEMS.Add(item);

						}

						db.SaveChanges();

					tr.Complete();

					}
				}
			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al grabar planificación de tejeduría ", csEstado.eRelevancia.Grave);
			}
        }

		public static void CompletarPesaje(csMovimientos movPesaje)
		{
			Estado.Iniciar();

			try { 

				using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
				{
					using (System.Transactions.TransactionScope tr = new System.Transactions.TransactionScope()) {

						var mov = db.TBL_MOVCAB.Single(x => x.MOVCAB_CODEMP == movPesaje.MOVCAB_CODEMP &&
												x.MOVCAB_CODMOV == movPesaje.MOVCAB_CODMOV &&
												x.MOVCAB_NROMOV == movPesaje.MOVCAB_NROMOV);

						var items = db.TBL_MOVITEMS.Where(y => y.MOVITEMS_CODEMP == movPesaje.MOVCAB_CODEMP &&
															   y.MOVITEMS_CODMOV == movPesaje.MOVCAB_CODMOV &&
															   y.MOVITEMS_NROMOV == movPesaje.MOVCAB_NROMOV);
	
						mov.MOVCAB_ESTADO = csEstados.EstadosMovimiento.TERMINADO;

						mov.MOVCAB_NETO = items.Sum(x => x.MOVITEMS_NETO);
						mov.MOVCAB_BRUTO = items.Sum(x => x.MOVITEMS_BRUTO);
						mov.MOVCAB_TARA = items.Sum(x => x.MOVITEMS_TARA);
						mov.MOVCAB_MTS = items.Sum(x => x.MOVITEMS_CNTSEC);
						mov.MOVCAB_CANTBULBAL = items.Count();
						mov.MOVCAB_CANTBULCPTEORI = items.Count();
						mov.MOVCAB_OBSERVACIONES = movPesaje.MOVCAB_OBSERVACIONES;

						db.SaveChanges();
						tr.Complete();
					}
				}

			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al completar pesaje", csEstado.eRelevancia.Grave);
			}
		}

		//public static bool ProcesarPesaje(csMovimientos movPesaje)
		//{
		//	ProduccionBI.ConexionDB conexion = new ProduccionBI.ConexionDB();
		//	conexion.AgregarParametro("@CODEMP", System.Data.SqlDbType.VarChar, 30, movPesaje.MOVCAB_CODEMP);
		//	conexion.AgregarParametro("@CODMOV", System.Data.SqlDbType.VarChar, 30, movPesaje.MOVCAB_CODMOV);
		//	conexion.AgregarParametro("@NROMOV", System.Data.SqlDbType.Int, movPesaje.MOVCAB_NROMOV);
		//	conexion.AgregarParametro("@CODEMP_DEST", System.Data.SqlDbType.VarChar, 30, "GCA1");
		//	conexion.AgregarParametro("@COD_INTEQM", System.Data.SqlDbType.VarChar, 30, "ING_TJ");
		//	conexion.BuscarResultadosConParametros("DECLARE @RETVAL INT; EXEC @RETVAL = SP_INTER_PROCESAR @CODEMP, @CODMOV, @NROMOV, @CODEMP_DEST, @COD_INTEQM ; SELECT @RETVAL ");

		//	if (conexion.Estado.HayError)
		//	{
		//		throw new Exception(conexion.Estado.Mensaje);
		//	}

		//	return conexion.ObtenerResultados().Rows[0][0].ToString().Equals("1");

		//}

		public static void EliminarPiezaPesaje(csMovimientos movPesaje, csItems item)
		{
			using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
			{
				using(System.Transactions.TransactionScope tr = new System.Transactions.TransactionScope()) {

					var stock = (from a in db.TBL_STRMVK
								 where a.STRMVK_TIPPRO == item.MOVITEMS_TIPPRO &&
									   a.STRMVK_ARTCOD == item.MOVITEMS_ARTCOD &&
									   a.STRMVK_NSERIE == item.MOVITEMS_NSERIE
								select a).FirstOrDefault();

					db.TBL_STRMVK.Remove(stock);

					var movitem = db.TBL_MOVITEMS.Where(x => x.MOVITEMS_CODEMP == item.MOVITEMS_CODEMP && 
															 x.MOVITEMS_CODMOV == item.MOVITEMS_CODMOV && 
															 x.MOVITEMS_NROMOV == item.MOVITEMS_NROMOV &&
															 x.MOVITEMS_NSERIE == item.MOVITEMS_NSERIE).FirstOrDefault();
					db.TBL_MOVITEMS.Remove(movitem);
					db.SaveChanges();
					tr.Complete();
                }
			}
		}

		public static csItems GrabarItemPesaje(csMovimientos movPesaje, csItems itemPesaje)
		{

			Estado.Iniciar();
			csItems item = new csItems();

			try
			{ 
				item.MOVITEMS_CODMOV = csEstados.TipoMovimiento.PROBTJ;
				item.MOVITEMS_CODEMP = Seguridad.BI.csUsuario.Empresa;
				item.MOVITEMS_NROMOV = movPesaje.MOVCAB_NROMOV;
				item.MOVITEMS_PLANTA = Seguridad.BI.csPlantas.MiPlanta.PC_PLANTA_COD;
				item.MOVITEMS_BRUTO = itemPesaje.MOVITEMS_BRUTO;
				item.MOVITEMS_NETO = itemPesaje.MOVITEMS_NETO;
				item.MOVITEMS_TARA = itemPesaje.MOVITEMS_TARA;
				item.MOVITEMS_CANTID = itemPesaje.MOVITEMS_CANTID;
				item.MOVITEMS_CNTSEC = itemPesaje.MOVITEMS_CNTSEC;
				item.MOVITEMS_TIPPRO = itemPesaje.MOVITEMS_TIPPRO;
				item.MOVITEMS_ARTCOD = itemPesaje.MOVITEMS_ARTCOD;
				item.MOVITEMS_DESCRP = itemPesaje.MOVITEMS_DESCRP;
				item.MOVITEMS_ENVASE = csEnvase.CRUDO;
				item.MOVITEMS_TIPOEMBAL = "ROLLO";
				item.MOVITEMS_ESTADO = csEstados.EstadosMovimiento.TERMINADO;
				item.MOVITEMS_FALLA_ID = itemPesaje.MOVITEMS_FALLA_ID;
				item.MOVITEMS_FECALT = DateTime.Now;
				item.MOVITEMS_NATRIB = csPartida.SIN_PARTIDA;
				item.MOVITEMS_NOTROS = itemPesaje.MOVITEMS_NOTROS;
				item.MOVITEMS_NSERIE = ProduccionBI.GestionBalanza.Balanza.UltimoNro;
				item.MOVITEMS_UNIMED = "K";
				item.MOVITEMS_PESAJE_MANUAL = false;
				item.MOVITEMS_DEPOSI= itemPesaje.MOVITEMS_DEPOSI;
				item.MOVITEMS_SECTOR = itemPesaje.MOVITEMS_SECTOR;
				item.MOVITEMS_TRANSFERIDO = false;
				item.MOVITEMS_PRINCIPAL = false;
				item.MOVITEMS_VUELTASXMINUTO = 0;
				item.MOVITEMS_VUELTASXROLLO = 0;
				item.MOVITEMS_PORCENTAJE = 0;
				item.MOVITEMS_TEJE_TEJEDOR = itemPesaje.MOVITEMS_TEJE_TEJEDOR;
				item.MOVITEMS_TEJE_REVISADOR = itemPesaje.MOVITEMS_TEJE_REVISADOR;

				item.Grabar(item);

				ProduccionBI.GestionBalanza.ActualizarUltimoNro();

				if (ProduccionBI.GestionBalanza.Estado.HayError)
				{
					throw new Exception(ProduccionBI.GestionBalanza.Estado.Mensaje);
				}

			}catch(Exception ex)
			{
				Estado.CapturarError(ex, "Error al grabar item de pesaje tejeduría", csEstado.eRelevancia.Grave);
			}

            return item;
        }

		public static void GrabarItemPesaje(csMovimientos movPesaje)
		{
			throw new NotImplementedException();
		}

		public static csMovimientos  GrabarPesaje(csProducto producto, int nroProg)
		{
			using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion)) { 

				DAL.TBL_MOVCAB movcab = new DAL.TBL_MOVCAB();
				movcab.MOVCAB_CODEMP = Seguridad.BI.csUsuario.Empresa;
				movcab.MOVCAB_CODMOV = csEstados.TipoMovimiento.PROBTJ;
				movcab.MOVCAB_PLANTA = Seguridad.BI.csPlantas.MiPlanta.PC_PLANTA_COD;
				movcab.MOVCAB_PROG_NRO = nroProg;
				try
				{
					var movs = db.TBL_MOVCAB.Where(x => x.MOVCAB_CODEMP == movcab.MOVCAB_CODEMP && x.MOVCAB_CODMOV == movcab.MOVCAB_CODMOV);
					movcab.MOVCAB_NROMOV = (movs.Count() == 0 ? 1 : movs.Max(y => y.MOVCAB_NROMOV) + 1);
				}
				catch (Exception) { movcab.MOVCAB_NROMOV = 1; }

				movcab.MOVCAB_TIPPRO = producto.STMPDH_TIPPRO;
				movcab.MOVCAB_ARTCOD = producto.STMPDH_ARTCOD;
				movcab.MOVCAB_TIPOCPTEORI = "REM";
				movcab.MOVCAB_CANTKGCPTEORI = 0;
				movcab.MOVCAB_CANTBULCPTEORI = 0;
				movcab.MOVCAB_CANTKGBAL = 0;
				movcab.MOVCAB_CANTBULBAL = 0;
				movcab.MOVCAB_FECALT = DateTime.Now;
				movcab.MOVCAB_USERID = Seguridad.BI.csUsuario.Login;
				movcab.MOVCAB_ESTADO = csEstados.EstadosMovimiento.HABILITADO;
				movcab.MOVCAB_OBJETADO = false;
				movcab.MOVCAB_EXCEPCION = string.Empty;
				movcab.MOVCAB_PESAJE_MANUAL = false;
				movcab.MOVCAB_TARA_UNIDAD = 0;
				movcab.MOVCAB_MAQUINA = Environment.MachineName;

				db.TBL_MOVCAB.Add(movcab);
				db.SaveChanges();

				return new csMovimientos
				{
					MOVCAB_CODEMP = movcab.MOVCAB_CODEMP,
					MOVCAB_PLANTA = movcab.MOVCAB_PLANTA,
					MOVCAB_CODMOV = movcab.MOVCAB_CODMOV,
					MOVCAB_NROMOV = movcab.MOVCAB_NROMOV
				};
			}
		}


		public static List<csItems> GetAllByNroPrograma(int nroProg)
		{
			List<csItems> ListaItems = new List<csItems>();

			using(DAL.GeshilEntities db = new DAL.GeshilEntities(Seguridad.BI.csParametros.Conexion))
			{
					ListaItems = (from a in db.TBL_MOVCAB.Where(x => x.MOVCAB_CODEMP == Seguridad.BI.csUsuario.Empresa && x.MOVCAB_PROG_NRO == nroProg && x.MOVCAB_CODMOV == csEstados.TipoMovimiento.PLANTJ).ToList()
								  select new csItems
								  {
									  MOVITEMS_CODEMP = a.MOVCAB_CODEMP,
									  MOVITEMS_CODMOV = a.MOVCAB_CODMOV,
									  MOVITEMS_NROMOV = a.MOVCAB_NROMOV

								  }).ToList();
			}

			return ListaItems;
        }


	}

	public class csMaquinaTejeduria
	{
		public int TEJE_MAQUINA_ID { get; set; }
		public string TEJE_MAQUINA_DESCRP { get; set; }
	}
}
