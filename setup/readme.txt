Setup Instructions for the Export Import Tool

1. Clone the Git Repositpory, get the code.
2. Compile the code using Visual Studio 2012.
3. Once code is compilled execute it. A new window will pop up.
4. Browse the desired CSV file with following criteriaÖ
    A. Comma separated
    B. With assumption that there are no headers
    C. Columns are in sequence - Account, Description, Country Code, Value
5. If don't have the csv try one in rawData folder.
6. Then validate the file - the system will give details about the total rows in CSV, correctly formatted and faulty rows.
7. Faulty rows will be displayed in the table.
8. Provide the credentials to connect to the SQL DB. The Data will be stored in the table named "taxes" for demo purposes.
9. You can only start the import process once user is authenticated. The successful authentication is indicated by turning connection
button to green.
10. Once the export to database is done the system will show a message alert.
11. The UI of the system is self explaining the usage by highlighting the buttons for the next step.