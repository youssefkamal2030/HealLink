import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import '../../../../../../core/utils/app_images.dart';

class CustomCircleImage extends StatelessWidget {
  const CustomCircleImage({
    super.key,
    required this.image,
    required this.isVerified,
    required this.radius,
  });

  final String image;
  final bool isVerified;
  final double radius;

  @override
  Widget build(BuildContext context) {
    return Stack(
      alignment: AlignmentDirectional.bottomEnd,
      children: [
        CircleAvatar(
          radius: radius,
          child: ClipOval(child: SvgPicture.asset(image, fit: BoxFit.cover)),
        ),
        if (isVerified)
          SvgPicture.asset(AppImages.verified, height: 22, width: 22),
      ],
    );
  }
}
