class TitleAndSubtitleModel {
  final String title;
  final String subtitle;

  TitleAndSubtitleModel({required this.title, required this.subtitle});

  factory TitleAndSubtitleModel.fromJson(Map<String, dynamic> json) {
    return TitleAndSubtitleModel(
      title: json['title'] ?? '',
      subtitle: json['subtitle'] ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {'title': title, 'subtitle': subtitle};
  }

  @override
  String toString() => 'Title: $title, Subtitle: $subtitle';
}
