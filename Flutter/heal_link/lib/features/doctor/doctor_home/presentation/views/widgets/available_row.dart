import 'package:flutter/material.dart';
import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../core/utils/function/app_colors.dart';
import '../../../../../../generated/l10n.dart';

class AvailableRow extends StatefulWidget {
  const AvailableRow({super.key});

  @override
  State<AvailableRow> createState() => _AvailableRowState();
}

class _AvailableRowState extends State<AvailableRow> {
  bool isSwitched = true;
  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.end,
      spacing: 20,
      children: [
        Text(
          S.of(context).available_now,
          style: AppTextStyles.popins500style18LightBlackColor.copyWith(
            fontSize: 12,
            color: AppColors.kPrimaryColor,
          ),
        ),
        GestureDetector(
          onTap: () {
            setState(() {
              isSwitched = !isSwitched;
            });
          },
          child: Stack(
            alignment: Alignment.centerLeft,
            children: [
              // Track (white)
              AnimatedContainer(
                duration: Duration(milliseconds: 200),
                width: 38,
                height: 15,
                decoration: BoxDecoration(
                  color: isSwitched ? Colors.white : Colors.grey.shade400,
                  borderRadius: BorderRadius.circular(15),
                ),
              ),
              // Thumb (green circle) with lift
              AnimatedPositioned(
                duration: Duration(milliseconds: 200),
                left: isSwitched ? 20 : 0,
                child: Container(
                  height: 18,
                  width: 18,
                  decoration: BoxDecoration(
                    color: Color(0xff48B158),
                    shape: BoxShape.circle,
                    boxShadow: [
                      BoxShadow(
                        color: Colors.black12,
                        blurRadius: 2,
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
        ),
        SizedBox(width: 0),
      ],
    );
  }
}
