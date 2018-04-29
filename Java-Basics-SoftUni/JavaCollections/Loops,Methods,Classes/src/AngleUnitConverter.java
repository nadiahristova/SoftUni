import java.util.Scanner;

public class AngleUnitConverter {
       
	private static Scanner scan;
        public static void main(String[] args) {
                scan = new Scanner(System.in);
                int count = Integer.parseInt(scan.nextLine());
                String[] commands = new String[count*2];
                int lastCommandIndex = 0;
                for(int i = 0 ; i < count ; i++){
                           String line = scan.nextLine();
                           String[] lineAsArray = line.split(" ");
                           String inputValue = lineAsArray[0];
                           commands[lastCommandIndex] = inputValue;
                       String typeOfConvertion = lineAsArray[1];
                       char[] typeOfConvertionCharArray = typeOfConvertion.toCharArray();
                           if(typeOfConvertionCharArray[0]=='r') commands[lastCommandIndex + 1] = "r";
                           else commands[lastCommandIndex+1] = "d";
                           lastCommandIndex += 2;
                        }
                lastCommandIndex = 0;
                for(int i = 0 ;i<count; i++){
                         if(commands[lastCommandIndex+1]=="r") {
                                 System.out.format("%.6f deg", (Math.toDegrees(Double.parseDouble(commands[lastCommandIndex]))));
                                 System.out.println();
                                 }
                         else {
                                   System.out.format("%.6f rad",Math.toRadians(Double.parseDouble(commands[lastCommandIndex])));
                                   System.out.println();
                         }
                         lastCommandIndex +=2;
                }
        }
}