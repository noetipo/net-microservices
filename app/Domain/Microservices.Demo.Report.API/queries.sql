-- Demo.Policy
SELECT p.PolicyId, p.ProductCode, p.AgentLogin, p.PolicyStatusId, p.Description, p.ProductStatusId 
	FROM Policy p
		LEFT OUTER JOIN PolicyStatus ps ON p.PolicyStatusId = ps.PolicyStatusId 
-- Demo.Product
SELECT p.Code, p.Name, p.Description, p.Image, p.MaxNumberOfInsured, ps.Description 
	FROM Product p
		LEFT OUTER JOIN ProductStatus ps ON p.ProductStatusId = ps.ProductStatusId
--

