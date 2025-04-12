gci -Recurse ./services | % {kubectl delete -f $_.FullName}
# gci -Recurse ./volumes | % {kubectl delete -f $_.FullName}
gci -Recurse ./app | % {kubectl delete -f $_.FullName}
