import jsPDF from 'jspdf';
import 'jspdf-autotable';


export default function descargarPDF(titulosArray, contenidoArray, titulo){
    const doc = new jsPDF({
      orientation: 'landscape'
    });

    const datosTabla = [];
    const titulosTabla = titulosArray.map(item => {
      if(item.title != 'OPCIONES'){
        return item.title
      }
    })
    datosTabla.push(titulosTabla);
  
    contenidoArray.forEach(item => {
      const fila = [];
      Object.keys(item).forEach(key => {
        fila.push(item[key]);
      });
      datosTabla.push(fila);
    });
  
    doc.autoTable({
      head: [datosTabla[0]],
      body: datosTabla.slice(1)
    });

    doc.text(`Listado de ${titulo}`, 10, 10);
  
    doc.save(`tabla-${titulo}.pdf`);
}