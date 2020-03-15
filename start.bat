REM Masks for affinity
REM https://stackoverflow.com/questions/7759948/set-affinity-with-start-affinity-command-on-windows-7
REM CPU ID  CPU value (dec)
REM 0       001 (= 2^0)
REM 1       002 (= 2^1)
REM 2       004 (= 2^2)
REM 3       008 (= 2^3)
REM 4       016 (= 2^4)
REM 5       032 (= 2^5)
REM 6       064 (= 2^6)
REM 7       128 (= 2^7)


REM Default start
start /affinity 001 KSR.exe

REM Start with
REM -------------------Printing settings------------------------------
REM {
REM BinaryArticleBodyFeature,
REM KeyWordsFirstParagraphArticleBodyFeature,
REM Binary Function,
REM Niewiadomski Function,
REM }
REM Testing percentage: 60, Learning percentage: 40
REM kNN neighbors: 10
REM Chosen keyWordsExtractor: KSR.Tools.Frequency.TFFrequency
REM ------------------------------------------------------------------

start /affinity 002 KSR.exe -f1 tfft -f2 tfffft -t 60 -l 40 -kwe 1 -k 10


REM -------------------Printing settings------------------------------
REM {
REM BinaryArticleBodyFeature,
REM SimilarityBodyFeature -> Binary Function,
REM SimliarityFirstParagraph -> Niewiadomski Function,
REM Simliarity30PercentBody -> Jaccard Function,
REM }
REM Testing percentage: 60, Learning percentage: 40
REM kNN neighbors: 10
REM Chosen keyWordsExtractor: KSR.Tools.Frequency.TFFrequency
REM ------------------------------------------------------------------

start /affinity 004 KSR.exe -f1 tfff -f2 tfffff -f3 ffffft -f4 ftffff -t 60 -l 40 -kwe 1 -k 10