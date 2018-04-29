import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;


public class Prob8_SumNumbersFromTextFile {
	public static void main(String[] args) {
		int sum = 0;
		String fileLine = null;
		
		try (InputStream fileInput = new FileInputStream("someFile.txt");
			 BufferedReader buffRead = new BufferedReader(new InputStreamReader(fileInput))) {
			Reader reader = new InputStreamReader(fileInput);			
			while ((fileLine = buffRead.readLine()) != null) {
				if (!fileLine.equals("")) {
					sum += Integer.parseInt(fileLine);					
				}				
			}
		} catch (IOException e) {
			System.out.println("Error.");
		}
		System.out.println(sum);
	}
}
