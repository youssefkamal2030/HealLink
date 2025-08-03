import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class InfoField extends StatefulWidget {
  const InfoField({
    super.key,
    required this.label,
    this.value,
    this.img,
    required this.isEditable,
  });

  final String label;
  final String? value;
  final String? img;
  final bool isEditable;

  @override
  State<InfoField> createState() => _InfoFieldState();
}

class _InfoFieldState extends State<InfoField> {
  bool isSwitchOn = true;

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 37,
      padding: const EdgeInsets.all(8),
      decoration: BoxDecoration(
        border: Border.all(width: 1, color: Colors.black.withOpacity(0.05)),
        borderRadius: const BorderRadius.all(Radius.circular(8)),
      ),
      child: Row(
        children: [
          Expanded(
            child: RichText(
              text: TextSpan(
                text: '${widget.label}: ',
                style: AppTextStyles.popins500style14LightBlackColor,
                children: [
                  if (widget.value != null)
                    TextSpan(
                      text: widget.value,
                      style: AppTextStyles.popins400style14LightBlackColor
                          .copyWith(color: AppColors.kDarkGreyColor),
                    ),
                ],
              ),
              overflow: TextOverflow.ellipsis,
            ),
          ),

          const SizedBox(width: 8),

          if (widget.isEditable)
            widget.img != null
                ? SvgPicture.asset(widget.img!, height: 16, width: 16)
                : Transform.scale(
                  scale: 0.8,
                  child: CupertinoSwitch(
                    value: isSwitchOn,
                    onChanged: (val) {
                      setState(() {
                        isSwitchOn = val;
                      });
                    },
                    activeTrackColor: AppColors.kPrimaryColor,
                  ),
                ),
        ],
      ),
    );
  }
}
