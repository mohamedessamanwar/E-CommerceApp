create view ProductView 
As
  Select P.Id , C.Id As CategoryId , P.Name , C.Name As CategoryName , P.CurrentPrice  
  From Products As P join categories As C On p.CategoryID = C.Id 
  where p.Id = 1 ;