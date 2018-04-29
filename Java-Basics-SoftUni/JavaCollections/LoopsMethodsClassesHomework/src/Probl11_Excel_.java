import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Map;
import java.util.TreeMap;

import org.apache.poi.ss.usermodel.Cell;
import org.apache.poi.ss.usermodel.Row;
import org.apache.poi.xssf.usermodel.XSSFSheet;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

public class Probl11_Excel_ {
	public static void main(String[] args) {
		 Map<String,Double> officeAndIncome = new TreeMap<>();	
		 try(FileInputStream file = new FileInputStream(new File("Incomes-Report.xlsx"));
				XSSFWorkbook workbook = new XSSFWorkbook(file)) { 	
			 
	            XSSFSheet sheet = workbook.getSheetAt(0);	            
	            java.util.Iterator<Row> rowIterator = sheet.iterator();	                        
	            Row row = rowIterator.next();
	            java.util.Iterator<Cell> cellIterator = row.cellIterator();  
	            int columnNum = -1;
	            int columnNumOffice = 0;
	            
	            while(cellIterator.hasNext()) {
	            	columnNum++;	                
	            	Cell cell = cellIterator.next();
	            	String str = cell.getStringCellValue();	                
	            	if (str.equals("Office")){
	            		for (int rowNum = 1; rowNum <= sheet.getLastRowNum() ; rowNum++) {
	            			Row currRow = sheet.getRow(rowNum);
							String officeName = currRow.getCell(columnNum).getStringCellValue();
							officeAndIncome.put(officeName, 0.0);							
						}
	            		columnNumOffice = columnNum;
					}
	            	
	            	if (str.equals("Total Incomes")){
	            		for (int rowNum = 1; rowNum <= sheet.getLastRowNum() ; rowNum++) {
	            			Row currRow = sheet.getRow(rowNum);
							Double officeIncome = currRow.getCell(columnNum).getNumericCellValue();
							String officeName = currRow.getCell(columnNumOffice).getStringCellValue();
							officeIncome+=officeAndIncome.get(officeName);	                		
							officeAndIncome.put(officeName, officeIncome);
						}
					}	                                                      
	            }	              	           
	            file.close();
	           
	            PrintWriter writer = new PrintWriter("/home/nadia/Downloads/totalIncomes.txt", "UTF-8");
	            Double totalIncome = 0D;
	            String line = null;
	            for(String office : officeAndIncome.keySet()){
					line = office + " -> " + String.format("%.2f", officeAndIncome.get(office));
					totalIncome += officeAndIncome.get(office);
					writer.println(line);
					System.out.println(line);
				}
	            line = "\nGrand Total -> " + String.format("%.2f", totalIncome);
	            writer.println(line);
	            System.out.println(line);
	            writer.close();	             

		 } catch (FileNotFoundException e) {
	        	System.out.println("File Not Found.");
	        } catch (IOException e) {
	            e.printStackTrace();
	        }
	}
}
