import { jsPDF } from "jspdf";
import "jspdf-autotable";

export default function descargarPDF(titulosArray, contenidoArray, titulo) {
  const doc = new jsPDF({
    orientation: 'landscape'
  });

  console.log(titulosArray);
  console.log(contenidoArray);

  const datosTabla = [];
  const titulosTabla = titulosArray.map(item => item.title).filter(title => title !== 'OPCIONES');
  datosTabla.push(titulosTabla);

  contenidoArray.forEach(item => {
    const fila = titulosArray.map(titulo => {
      if (titulo.key !== 'opciones') {
        return item[titulo.key];
      }
    }).filter(value => value !== undefined);
    datosTabla.push(fila);
  });

  doc.autoTable({
    head: [datosTabla[0]],
    body: datosTabla.slice(1)
  });

  doc.text(`Listado de ${titulo}`, 10, 10);

  doc.save(`tabla-${titulo}.pdf`);
}