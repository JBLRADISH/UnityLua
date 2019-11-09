local Quaternion = {}

function Quaternion.New(x, y, z, w)				
	local q = {x = x or 0, y = y or 0, z = z or 0, w = w or 0}					
	return q
end

return Quaternion