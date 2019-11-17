function hotfix(class, name, func)
	class[name.."HotFix"] = func
end

hotfix(HotFix, "Add", function(self,a,b)
	print (self.name)
    return (a + b) * 5
end)